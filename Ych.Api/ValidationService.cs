using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ych.Configuration;

namespace Ych.Api
{
    // Defines Valid Lot Number types
    public enum LotNumberTypes
    {
        Harvest,
        Production,
        Any
    }

    /// <summary>
    /// Provides access to common validation functions and rules.
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validates one or more lot numbers and throws an exception if any of them are invalid.
        /// </summary>
        /// <param name="lotNumbers"></param>
        void ValidateLotNumbers(LotNumberTypes lotType, params string[] lotNumbers);

        /// <summary>
        /// Verifies if a lot number matches the expected regex.
        /// </summary>
        bool IsHarvestLotNumberValid(string lotNumber);

        /// <summary>
        /// Verifies if a lot number matches the expected regex.
        /// </summary>
        bool IsProductionLotNumberValid(string lotNumber);

        /// <summary>
        /// Validates one or more grower ids and throws an exception if any of them are invalid.
        /// </summary>
        /// <param name="growerIds"></param>
        void ValidateGrowerIds(params string[] growerIds);

        /// <summary>
        /// Verifies if a grower id matches the expected regex.
        /// </summary>
        bool IsGrowerIdValid(string growerId);

        /// <summary>
        /// Replaces year parameter with default value if empty and default is specified. Otherwise verifies if a year is a valid integer and matches expected regex.
        /// </summary>
        int ValidateYear(string year, int? valueIfEmpty = null);

        /// <summary>
        /// Verifies if a variety code matches the expected regex.
        /// </summary>
        bool IsVarietyCodeValid(string varietyCode);

        /// <summary>
        /// Validates one or more variety codes and throws an exception if any of them are invalid.
        /// </summary>
        /// <param name="varietyCodes"></param>
        void ValidateVarietyCodes(params string[] varietyCodes);

        /// <summary>
        /// Validates and parses an integer from a string and throws an exception if unable to validate.
        /// </summary>
        /// <param name="integer"></param>
        /// <param name="valueIfEmpty"></param>
        int ValidateInteger(string integer, int? valueIfEmpty = null);

        /// <summary>
        /// Validates and parses a bool from a string
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="valueIfEmpty"></param>
        bool ValidateBool(string boolean, bool valueIfEmpty);

        /// <summary>
        /// Validates a string is not empty throws an exception if unable to validate.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="variableName"></param>
        /// <param name="valueIfEmpty"></param>
        void ValidateFilled(string value, string variableName);

        /// <summary>
        /// Validates and parses a double from a string and throws an exception if unable to validate.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="valueIfEmpty"></param>
        double ValidateDouble(string number, double? valueIfEmpty = null);

        /// <summary>
        /// Validates one or more customer codes and throws an exception if any of them are invalid.
        /// </summary>
        /// <param name="customerCodes"></param>
        void ValidateCustomerCodes(params string[] customerCodes);

        /// <summary>
        /// Verifies if a customer code matches the expected regex.
        /// </summary>
        bool IsCustomerCodeValid(string customerCode);

        /// <summary>
        /// Verifies if a request form contains all required notification form keys
        /// </summary>
        void ValidateNotificationForm(ICollection<string> formKeys);

        /// <summary>
        /// Verifies if a work order code matches the expected regex.
        /// </summary>
        bool IsWorkOrderCodeValid(string workOrderCode);
    }

    public class ValidationService : IValidationService
    {
        public const string
            HarvestLotNumberRegexPattern = "^(?i)[0-9]{2}-[a-z]{2}[0-9]{3}-[0-9]{3}[a-z]?$"; // Is YY-SS###-####

        public const string ProductionLotNumberRegexPattern = @"^[A-Za-z\d\-]+$"; // Is alphanumeric lot number
        public const string GrowerIdRegexPattern = @"^\w{3}[0-9]{3}$"; // Is ABC###
        public const string YearRegexPattern = @"^\d{4}$"; // Is 4 digit int
        public const string VarietyCodeRegexPattern = @"^[A-Za-z\d]+$"; // Is alphanumeric
        public const string WorkOrderCodeRegexPattern = @"^WO{3}[0-9]-{6}[0-9]$"; // WOxxx-xxxxxx
        
        public readonly string[] NotificationRequiredKeys =
        {
            "notificationTypeId",
            "identifierType",
            "identifier"
        };

        private Regex harvestlotNumberRegex = new Regex(HarvestLotNumberRegexPattern);
        private Regex productionlotNumberRegex = new Regex(ProductionLotNumberRegexPattern);
        private Regex growerIdRegex = new Regex(GrowerIdRegexPattern);
        private Regex yearRegex = new Regex(YearRegexPattern);
        private Regex varietyCodeRegex = new Regex(VarietyCodeRegexPattern);
        private Regex workOrderCodeRegex = new Regex(WorkOrderCodeRegexPattern);
        private bool shouldValidateCustomerCodes;
        private Regex customerCodeRegex;

        public ValidationService(ISettingsProvider settings)
        {
            shouldValidateCustomerCodes = settings.GetValue(Config.Settings.Api().Validation().ValidateCustomerCodes(), false);

            try
            {
                customerCodeRegex = new Regex(settings.GetValue(Config.Settings.Api().Validation().ValidateCustomerCodeRegex(), @"^\w{3}[0-9]{3}$")); // Is ABC###
            }
            catch
            {
                shouldValidateCustomerCodes = false;
                customerCodeRegex = null;
            }
        }

        public void ValidateLotNumbers(LotNumberTypes lotType, params string[] lotNumbers)
        {
            List<(string, object, string)> failures = new List<(string, object, string)>();

            foreach (string lotNumber in lotNumbers)
            {
                if (lotType == LotNumberTypes.Harvest && !IsHarvestLotNumberValid(lotNumber))
                {
                    failures.Add(("lotNumber", lotNumber,
                        $"Does not match the expected pattern for harvest lots {HarvestLotNumberRegexPattern}"));
                }
                else if (lotType == LotNumberTypes.Production && !IsProductionLotNumberValid(lotNumber))
                {
                    failures.Add(("lotNumber", lotNumber,
                        $"Does not match the expected pattern for production lots {ProductionLotNumberRegexPattern}"));
                }
                else if (lotType == LotNumberTypes.Any && !IsHarvestLotNumberValid(lotNumber) &&
                         !IsProductionLotNumberValid(lotNumber))
                {
                    failures.Add(("lotNumber", lotNumber,
                        $"Does not match the expected pattern for harvest or production lots {HarvestLotNumberRegexPattern}, {ProductionLotNumberRegexPattern}"));
                }
            }

            if (failures.Count > 0)
            {
                throw new ApiValidationException(failures.ToArray());
            }
        }

        public bool IsHarvestLotNumberValid(string lotNumber)
        {
            if (string.IsNullOrWhiteSpace(lotNumber))
            {
                return false;
            }

            return harvestlotNumberRegex.Match(lotNumber).Success;
        }
        
        public bool IsWorkOrderCodeValid(string workOrderCode)
        {
            if (string.IsNullOrWhiteSpace(workOrderCode))
            {
                return false;
            }

            return workOrderCodeRegex.Match(workOrderCode).Success;
        }

        public bool IsProductionLotNumberValid(string lotNumber)
        {
            if (string.IsNullOrWhiteSpace(lotNumber))
            {
                return false;
            }

            return productionlotNumberRegex.Match(lotNumber).Success;
        }

        public void ValidateGrowerIds(params string[] growerIds)
        {
            List<(string, object, string)> failures = new List<(string, object, string)>();

            foreach (string growerId in growerIds)
            {
                if (!IsGrowerIdValid(growerId))
                {
                    failures.Add(("growerId", growerId, $"Does not match the expected pattern {GrowerIdRegexPattern}"));
                }
            }

            if (failures.Count > 0)
            {
                throw new ApiValidationException(failures.ToArray());
            }
        }

        public bool IsGrowerIdValid(string growerId)
        {
            if (string.IsNullOrWhiteSpace(growerId))
            {
                return false;
            }

            return growerIdRegex.Match(growerId).Success;
        }

        public int ValidateYear(string year, int? valueIfEmpty = null)
        {
            if (string.IsNullOrWhiteSpace(year) && valueIfEmpty != null)
            {
                return valueIfEmpty.Value;
            }
            else if (yearRegex.Match(year).Success && int.TryParse(year, out int result))
            {
                return result;
            }
            else
            {
                throw new ApiValidationException("year", year,
                    $"Does not match the expected pattern {YearRegexPattern}");
            }
        }

        public void ValidateVarietyCodes(params string[] varietyCodes)
        {
            List<(string, object, string)> failures = new List<(string, object, string)>();

            foreach (string varietyCode in varietyCodes)
            {
                if (!IsVarietyCodeValid(varietyCode))
                {
                    failures.Add(("varietyCode", varietyCode,
                        $"Does not match the expected pattern {VarietyCodeRegexPattern}"));
                }
            }

            if (failures.Count > 0)
            {
                throw new ApiValidationException(failures.ToArray());
            }
        }

        public bool IsVarietyCodeValid(string varietyCode)
        {
            if (string.IsNullOrWhiteSpace(varietyCode))
            {
                return false;
            }

            return varietyCodeRegex.Match(varietyCode).Success;
        }

        public int ValidateInteger(string integer, int? valueIfEmpty = null)
        {
            if (string.IsNullOrWhiteSpace(integer) && valueIfEmpty != null)
            {
                return valueIfEmpty.Value;
            }
            else if (int.TryParse(integer, out int result))
            {
                return result;
            }
            else
            {
                throw new ApiValidationException("integer", integer, "Is not a valid integer");
            }
        }

        public bool ValidateBool(string boolean, bool valueIfEmpty)
        {
            if (string.IsNullOrWhiteSpace(boolean))
            {
                return valueIfEmpty;
            }
            else
            {
                return boolean.ToLower() == "true" || boolean == "1";
            }
        }

        public void ValidateFilled(string value, string variableName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ApiValidationException("value", value, variableName + " is empty");
            }
        }

        public double ValidateDouble(string number, double? valueIfEmpty = null)
        {
            if (string.IsNullOrWhiteSpace(number) && valueIfEmpty != null)
            {
                return valueIfEmpty.Value;
            }
            else if (double.TryParse(number, out double result))
            {
                return result;
            }
            else
            {
                throw new ApiValidationException("double", number, "Is not a valid double");
            }
        }

        public void ValidateCustomerCodes(params string[] customerCodes)
        {
            List<(string, object, string)> failures = new List<(string, object, string)>();

            foreach (string customerCode in customerCodes)
            {
                if (!IsCustomerCodeValid(customerCode))
                {
                    failures.Add(("customerCode", customerCode,
                        $"Does not match the expected pattern {customerCodeRegex}"));
                }
            }

            if (failures.Count > 0)
            {
                throw new ApiValidationException(failures.ToArray());
            }
        }

        public bool IsCustomerCodeValid(string customerCode)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                return false;
            }
            else if (shouldValidateCustomerCodes && customerCodeRegex != null)
            {
                return customerCodeRegex.Match(customerCode).Success;
            }
            else
            {
                return true;
            }
        }

        public void ValidateNotificationForm(ICollection<string> formKeys)
        {
            ValidateForm(formKeys, NotificationRequiredKeys);
        }

        public void ValidateForm(ICollection<string> formKeys, string[] requiredKeys)
        {
            List<(string, object, string)> failures = new List<(string, object, string)>();

            foreach (var requiredKey in requiredKeys)
            {
                if (!formKeys.Contains(requiredKey))
                {
                    failures.Add(("Missing Form Parameter", null,
                        $"The request form is missing the required key {requiredKey}"));
                }
            }

            if (failures.Count > 0)
            {
                throw new ApiValidationException(failures.ToArray());
            }
        }
    }
}