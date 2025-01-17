using System;
using System.Collections.Generic;

namespace Ych.Api.Data.Selection.Models
{
    public partial class ProductionSensory
    {
        public ulong Id { get; set; }
        public ulong ProductionSampleId { get; set; }
        public string SampleCode { get; set; }
        public uint UserId { get; set; }
        public bool Validated { get; set; }
        public uint? Berry { get; set; }
        public bool BlackCurrant { get; set; }
        public bool Grape { get; set; }
        public bool Raspberry { get; set; }
        public bool Strawberry { get; set; }
        public string BerryOther { get; set; }
        public uint? StoneFruit { get; set; }
        public bool Cherry { get; set; }
        public bool Peach { get; set; }
        public string StoneFruitOther { get; set; }
        public uint? Pomme { get; set; }
        public bool Apple { get; set; }
        public bool Pear { get; set; }
        public string PommeOther { get; set; }
        public uint? Melon { get; set; }
        public bool Cantaloupe { get; set; }
        public bool Cucumber { get; set; }
        public bool Honeydew { get; set; }
        public bool Watermelon { get; set; }
        public string MelonOther { get; set; }
        public uint? Tropical { get; set; }
        public bool Banana { get; set; }
        public bool Coconut { get; set; }
        public bool Guava { get; set; }
        public bool Mango { get; set; }
        public bool PassionFruit { get; set; }
        public bool Pineapple { get; set; }
        public string TropicalOther { get; set; }
        public uint? Citrus { get; set; }
        public bool Grapefruit { get; set; }
        public bool Lemon { get; set; }
        public bool Lemongrass { get; set; }
        public bool Lime { get; set; }
        public bool Orange { get; set; }
        public string CitrusOther { get; set; }
        public uint? Floral { get; set; }
        public bool Geranium { get; set; }
        public bool Rose { get; set; }
        public bool Soapy { get; set; }
        public string FloralOther { get; set; }
        public uint? Herbal { get; set; }
        public bool BlackTea { get; set; }
        public bool Dill { get; set; }
        public bool GreenTea { get; set; }
        public bool Mint { get; set; }
        public bool Rosemary { get; set; }
        public string HerbalOther { get; set; }
        public uint? Vegetal { get; set; }
        public bool Celery { get; set; }
        public bool GreenPepper { get; set; }
        public bool TomatoPlant { get; set; }
        public string VegetalOther { get; set; }
        public uint? Grassy { get; set; }
        public bool GreenGrass { get; set; }
        public bool Hay { get; set; }
        public string GrassyOther { get; set; }
        public uint? Earthy { get; set; }
        public bool Compost { get; set; }
        public bool Geosmin { get; set; }
        public bool Mushroom { get; set; }
        public bool Musty { get; set; }
        public bool Soil { get; set; }
        public string EarthyOther { get; set; }
        public uint? Woody { get; set; }
        public bool Cedar { get; set; }
        public bool Pine { get; set; }
        public bool Resinous { get; set; }
        public bool Sawdust { get; set; }
        public bool TeaTree { get; set; }
        public string WoodyOther { get; set; }
        public uint? Spicy { get; set; }
        public bool Anise { get; set; }
        public bool BlackPepper { get; set; }
        public bool Cinnamon { get; set; }
        public bool Clove { get; set; }
        public bool Ginger { get; set; }
        public string SpicyOther { get; set; }
        public uint? SweetAromatic { get; set; }
        public bool Bubblegum { get; set; }
        public bool Caramel { get; set; }
        public bool Creamy { get; set; }
        public bool Vanilla { get; set; }
        public string SweetAromaticOther { get; set; }
        public uint? OnionGarlic { get; set; }
        public bool Garlic { get; set; }
        public bool GreenOnion { get; set; }
        public bool Onion { get; set; }
        public string OnionGarlicOther { get; set; }
        public uint? Dank { get; set; }
        public uint? BurntRubber { get; set; }
        public uint? Cardboard { get; set; }
        public uint? Catty { get; set; }
        public uint? Cheesy { get; set; }
        public uint? Diesel { get; set; }
        public uint? PlasticWaxy { get; set; }
        public uint? Smoky { get; set; }
        public uint? Sulfur { get; set; }
        public uint? Sweaty { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ProductionSamples ProductionSample { get; set; }
        public virtual Users User { get; set; }
    }
}
