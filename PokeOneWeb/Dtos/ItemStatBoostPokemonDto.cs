namespace PokeOneWeb.Dtos
{
    public class ItemStatBoostPokemonDto
    {
        public string ItemName { get; set; }
        public string ItemResourceName { get; set; }
        public string ItemEffect { get; set; }
        public decimal AttackBoost { get; set; }
        public decimal DefenseBoost { get; set; }
        public decimal SpecialAttackBoost { get; set; }
        public decimal SpecialDefenseBoost { get; set; }
        public decimal SpeedBoost { get; set; }
        public decimal HitPointsBoost { get; set; }
    }
}
