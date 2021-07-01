using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl
{
    public class CommonFormatHelper
    {
        public static string GetNatureEffect(Nature nature)
        {
            var statBoost = nature.StatBoost;
            var effect = "";

            if (statBoost.Attack != 0)
            {
                effect = statBoost.Attack > 0 ? "+Atk" + effect : effect + " / -Atk";
            }
            if (statBoost.SpecialAttack != 0)
            {
                effect = statBoost.SpecialAttack > 0 ? "+SpA" + effect : effect + " / -SpA";
            }
            if (statBoost.Defense != 0)
            {
                effect = statBoost.Defense > 0 ? "+Def" + effect : effect + " / -Def";
            }
            if (statBoost.SpecialDefense != 0)
            {
                effect = statBoost.SpecialDefense > 0 ? "+SpD" + effect : effect + " / -SpD";
            }
            if (statBoost.Speed != 0)
            {
                effect = statBoost.Speed > 0 ? "+Spe" + effect : effect + " / -Spe";
            }

            return effect;
        }
    }
}
