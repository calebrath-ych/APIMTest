using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ych.Api.Ycrm;
using Ych.Api.Data.Ycrm.Models;
using Ych.Api.Logging;
using Ych.Data;
using Ych.Logging;
using Ych.Api.Data.Ycrm;
using Ych.Configuration;
using System.IO;
using Castle.Core.Internal;
using Stubble.Core.Builders;
using Newtonsoft.Json.Linq;
using Ych.Api.Data;

namespace Ych.Api.Ycrm
{
    /// <summary>
    /// Contract for a DI compatible component/service. DI requires an interface and one or more implementations.
    /// For single implementation contracts, the interface and class can remain in the same file for simplicity.
    /// For interfaces with multiple implementations, the interface and all implementations should exist in separate files.
    /// </summary>
    public interface IYcrmService : IHealthyService
    {
        /// <summary>
        /// Get notifications for a given user
        /// </summary>
        Task<IEnumerable> GetYcrmUserNotifications(int userId, int page, int limit);

        /// <summary>
        /// Get notifications settings for a given user
        /// </summary>
        Task<IEnumerable> GetYcrmUserNotificationSettings(int userId);

        /// <summary>
        /// Get account interactions for a given account
        /// </summary>
        Task<IEnumerable> GetAccountInteractions(string x3Id);
        
        /// <summary>
        /// Get account representatives for a given account
        /// </summary>
        Task<IEnumerable> GetCustomerRepresentatives(string x3Id);
    }

    /// <summary>
    /// Implementation of IYcrmService contract. Business logic should exist in services and components like these
    /// rather than directly in the Azure functions. These services can then be injected into other services so long as they don't
    /// create circular dependencies.
    /// </summary>
    public class YcrmService : ApiDataService, IYcrmService
    {
        public const string YcrmSystemName = "Ycrm";
        public override string SystemName => YcrmSystemName;

        protected override ApiDataSource Db => db;

        private YcrmDataSource db;
        private ILogWriter log;
        private ISettingsProvider settings;

        public YcrmService(ISettingsProvider settings, YcrmDataSource db, ILogWriter log)
        {
            this.db = db;
            this.log = log;
            this.settings = settings;
        }

        public async Task<IEnumerable> GetYcrmUserNotifications(int userId, int page, int limit)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetYcrmUserNotifications: {userId} {page} {limit}"));

            // Raw SQL statement
            string sql = @"select notifications.id,
                                  notifications.context,
                                  notification_types.name      as notification_type_name,
                                  notification_types.icon         notification_type_icon,
                                  notification_types.notification_subject,
                                  notification_types.message_template as message,
                                  notification_types.help_text as notification_type_help_text,
                                  user_notification.push_sent,
                                  user_notification.email_sent,
                                  user_notification.push_read,
                                  user_notification.emails_processed
                            from notifications
                                 join user_notification on notifications.id = user_notification.notification_id
                                 join notification_types on notifications.notification_type_id = notification_types.id
                            where user_notification.user_id = ?
                            order by push_sent desc
                            limit ?
                            offset ?";

            //use SqlQueryToList to execute query and return results
            var notifications = await db.SqlQueryToList(sql, userId, limit, limit * page).ConfigureAwait(false);

            var stubble = new StubbleBuilder().Configure(settings => { settings.SetIgnoreCaseOnKeyLookup(true); })
                .Build();

            foreach (var notification in notifications)
            {
                notification["context"] = JObject.Parse(notification["context"].ToString());
                notification["message"] = stubble.Render(notification["message"].ToString(),
                    JObject.Parse(notification["context"].ToString()));
            }

            return notifications;
        }

        public async Task<IEnumerable> GetYcrmUserNotificationSettings(int userId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetYcrmUserNotificationSettings: {userId}"));

            // Raw SQL statement
            string sql = @"select user_notification_settings.notification_type_id,
                               notification_types.name,
                               notification_types.icon,
                               notification_types.notification_subject,
                               notification_types.help_text,
                               user_notification_settings.email,
                               user_notification_settings.push
                        from notification_types
                                 right outer join user_notification_settings
                                                  on user_notification_settings.notification_type_id = notification_types.id
                        where user_id = ?";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, userId).ConfigureAwait(false);
        }
        
        public async Task<IEnumerable> GetCustomerRepresentatives(string x3Id)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetYcrmUserNotificationSettings: {x3Id}"));

            // Raw SQL statement
            string sql = @"select users.id,
                   users.first_name,
                   users.last_name,
                   users.email,
                   users.phone_number,
                   users.job_title,
                   users.biography,
                   users.x3_id,
                   users.avatar_path,
                   representative_types.name,
                   users.created_at
            from customers
                     join customers_users on customers.id = customers_users.customer_id
                     join users on customers_users.user_id = users.id
                     left join representative_types on users.representative_type_id = representative_types.id
            where customers.x3_id = ?";
            
            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, x3Id).ConfigureAwait(false);
        }

        public async Task<IEnumerable> GetAccountInteractions(string x3Id)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetAccountInteractions: {x3Id}"));

            // Raw SQL statement
            string sql = @"select interactions.id,
                                   ifnull(doing_business_as, legal_name) as customer_name,
                                   users.id as created_by,
                                   interaction_types.id                as type_id,
                                   interaction_types.name                as type_name,
                                   interactions.notes,
                                   interactions.contact_id,
                                   interactions.grower_services_update,
                                   interactions.interaction_at,
                                   interactions.created_at
                            from interactions
                                     join customers on interactions.customer_id = customers.id
                                     join users on interactions.created_by = users.id
                                     join interaction_types on interactions.type_id = interaction_types.id
                            where customers.x3_id = ?
                              and interactions.deleted_at is null
                            order by interaction_at desc";


            var interactions = await db.SqlQueryToList(sql, x3Id).ConfigureAwait(false);

            var typeSql = @"select interaction_types.id,
                                   interaction_types.name,
                                   interaction_types.icon,
                                   ifnull(font_awesome_unicode_lookups.unicode, 'f274') as icon_unicode
                            from interaction_types
                                     left join font_awesome_unicode_lookups on interaction_types.icon = font_awesome_unicode_lookups.name
                            where deleted_at is null";
            var types = await db.SqlQueryToList(typeSql).ConfigureAwait(false);
            if (interactions.Any())
            {
                string[] repIds = interactions.Select(x => x["created_by"].ToString()).ToArray();
                var interactionReps = await GetRepresentatives(repIds);

                foreach (var interaction in interactions)
                {
                    interaction["representative"] =
                        interactionReps.FirstOrDefault(x => x["id"].ToString() == interaction["created_by"].ToString());
                    interaction["type"] =
                        types.FirstOrDefault(x => x["id"].ToString() == interaction["type_id"].ToString());
                    
                    var attachement = await GetInteractionAttachment(Int32.Parse(interaction["id"].ToString()));
                    interaction["attachment"] = attachement.FirstOrDefault();
                }
            }

            return interactions;
        }

        private async Task<IEnumerable> GetContact(int contactId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetContact: {contactId}"));

            // Raw SQL statement
            string sql = @"select *
                            from contacts
                            where id = ?
                              and deleted_at is null";

            //use SqlQueryToList to execute query and return results
            return await db.SqlQueryToList(sql, contactId).ConfigureAwait(false);
        }

        private async Task<List<Dictionary<string, object>>> GetRepresentatives(string[] repIds)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetRepresentatives: {repIds}"));

            // Raw SQL statement
            string sql = $@"select distinct id, first_name, last_name, email, x3_id
                            from users
                            where id in ({string.Join(", ", repIds.Select(s => "?"))})
                              and deleted_at is null";


            return await db.SqlQueryToList(sql, repIds).ConfigureAwait(false);
        }

        private async Task<List<Dictionary<string, object>>> GetInteractionAttachment(int interactionId)
        {
            // Services like this can ask for a LogWriter to log specific information.
            log.Write(new ApiLogEntry(GetType().Name, LogSeverities.Debug,
                $"GetInteractionAttachment: {interactionId}"));

            // Raw SQL statement
            string sql = $@"select files.id,
                       files.path,
                       files.name,
                       files.type
                from interactions
                join fileables on fileables.fileable_id = interactions.id and fileable_type = 'App\\Models\\Interaction'
                            join files on fileables.file_id = files.id
                            where interactions.id = ?";

            return await db.SqlQueryToList(sql, interactionId).ConfigureAwait(false);
        }
    }
}