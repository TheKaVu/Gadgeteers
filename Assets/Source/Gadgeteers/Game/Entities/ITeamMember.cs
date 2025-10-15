namespace Source.Gadgeteers.Game.Entities
{
    public interface ITeamMember
    {
        public Team Team => Team.GetMembership(this);
    }
}