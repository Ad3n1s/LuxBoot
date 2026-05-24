using Microsoft.EntityFrameworkCore;

namespace LuxBoot.Models
{
    public class AccountInfoModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public UserApp User { get; set; }

        public string? MemberSince { get; set; }

        public string? CurrentPlan { get; set; }
        public int TotalAttacks { get; set; }
        public string? LastAttack { get; set; }
        public int AttacksLeft { get; set; }

        public string[] Attacks { get; set; } = {"0", "0", "0", "0", "0", "0", "0"};

        public List<AttackItem> CurrentAttacksList { get; set; } = new();
    }
}
