using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Data.Extensions
{
    public static class NatureExtensions
    {
        /// <summary>
        /// Returns a displayable description of the effect of the nature.
        /// </summary>
        public static string GetDescription(this Nature nature)
        {
            var effect = string.Empty;

            if (nature.Attack != 0)
            {
                effect = nature.Attack > 0 ? "+Atk" + effect : effect + " / -Atk";
            }

            if (nature.SpecialAttack != 0)
            {
                effect = nature.SpecialAttack > 0 ? "+SpA" + effect : effect + " / -SpA";
            }

            if (nature.Defense != 0)
            {
                effect = nature.Defense > 0 ? "+Def" + effect : effect + " / -Def";
            }

            if (nature.SpecialDefense != 0)
            {
                effect = nature.SpecialDefense > 0 ? "+SpD" + effect : effect + " / -SpD";
            }

            if (nature.Speed != 0)
            {
                effect = nature.Speed > 0 ? "+Spe" + effect : effect + " / -Spe";
            }

            return effect;
        }
    }
}