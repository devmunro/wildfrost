using System.Collections.Generic; // Ensure this namespace is included for List<T>
using System.Linq; // Ensure this namespace is included for Cast<T>
using Deadpan.Enums.Engine.Components.Modding; // Assuming this includes WildfrostMod, CardDataBuilder, StatusEffectDataBuilder, CardData, StatusEffectData
using UnityEngine.Localization; // Add this for LocalizedString

namespace WildfrostProject
{
    public class Tutorial1 : WildfrostMod
    {
        public Tutorial1(string modDirectory) : base(modDirectory)
        {
        }

        public override string GUID => "munro.wildfrost.tutorial";

        public override string[] Depends => new string[0];

        public override string Title => "My first wildfrost mod";

        public override string Description =>
            "This is my first mod for wildfrost :)";
        
        private List<CardDataBuilder> _cards;                 // The list of custom CardData(Builder)
        private List<StatusEffectDataBuilder> _statusEffects; // The list of custom StatusEffectData(Builder)
        private bool _preLoaded;                              // Used to prevent redundantly reconstructing our data. Not truly necessary.

        private void CreateModAssets()
        {
            _statusEffects = new List<StatusEffectDataBuilder>();

            // Code for status effects
            // Example: _statusEffects.Add(new StatusEffectDataBuilder(/* parameters */));

            _cards = new List<CardDataBuilder>();

            // Code for cards
            // Example: _cards.Add(new CardDataBuilder(/* parameters */));
            _cards.Add(
                new CardDataBuilder(this).CreateUnit("redQueen", "Red Queen") // Internally the card's name will be "[GUID].redQueen". In-game, it will be "Red Queen".
                    .SetSprites("redqueen.png", "redqueenbg.png")             // Sprites for the card.
                    .SetStats(8, 1, 3)                                        // Red Queen will have 8 health, 1 attack, and a 3-counter.
                    .WithCardType("Friendly")                                 // All companions are "Friendly". This line is not necessary since CreateUnit already sets the cardType to "Friendly".
                    .WithFlavour("I don't have an ability yet :/")
                    .AddPool("MagicUnitPool")                                 // This puts Red Queen in the Shademancer pools. Other choices were "GeneralUnitPool", "SnowUnitPool", "BasicUnitPool", and "ClunkUnitPool".
            );

            _preLoaded = true;
        }

        protected override void Load()
        {
            if (!_preLoaded) 
            {
                CreateModAssets();
            } 
            base.Load(); // Actual loading
        }

        protected override void Unload()
        {
            base.Unload();
        }

        public override List<T> AddAssets<T, Y>() // This method is called 6-7 times in base.Load() for each Builder.
        {
            var typeName = typeof(Y).Name;
            switch (typeName) // Checks what the current builder is
            {
                case nameof(CardData): 
                    return _cards.Cast<T>().ToList(); // Loads our cards
                case nameof(StatusEffectData):
                    return _statusEffects.Cast<T>().ToList(); // Loads our status effects
                default:
                    return null;
            }
        }
    }
}
