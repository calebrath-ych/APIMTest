using System;
using System.Collections.Generic;

namespace Ych.Api.Data.GrowerPortal.Models
{
    public partial class BrewerFeedback
    {
        public uint Id { get; set; }
        public string Agent { get; set; }
        public string Customer { get; set; }
        public string LotNumber { get; set; }
        public byte Ranking { get; set; }
        public byte Rating { get; set; }
        public byte Selected { get; set; }
        public byte SweetAromatic { get; set; }
        public byte Melon { get; set; }
        public byte Pomme { get; set; }
        public byte Berry { get; set; }
        public byte Evergreen { get; set; }
        public byte Woody { get; set; }
        public byte Nutty { get; set; }
        public byte Earthy { get; set; }
        public byte Spicy { get; set; }
        public byte Vegetal { get; set; }
        public byte Floral { get; set; }
        public byte Grassy { get; set; }
        public byte Herbal { get; set; }
        public byte Fruity { get; set; }
        public byte StoneFruit { get; set; }
        public byte Citrus { get; set; }
        public byte Tropical { get; set; }
        public byte Og { get; set; }
        public byte Cheesy { get; set; }
        public byte Plastic { get; set; }
        public byte Diesel { get; set; }
        public byte Sweaty { get; set; }
        public byte Sulfur { get; set; }
        public byte Smoky { get; set; }
        public byte Catty { get; set; }
        public byte Cardboard { get; set; }
        public byte BurntRubber { get; set; }
        public string SensoryNotes { get; set; }
        public string SelectionNotes { get; set; }
        public DateTime Submitted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
