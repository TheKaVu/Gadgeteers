using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Gadgeteers.Game.Entities
{
    [Serializable]
    public class Team
    {
        private static Dictionary<string, Team> _teams = new();
        
        private string _name;
        private Color _color;
        private HashSet<ITeamMember> _members = new();
        
        public string Name => _name;
        public Color Color => _color;
        public HashSet<ITeamMember> Members => new(_members);
        
        private Team(string name, Color color)
        {
            _name = name;
            _color = color;
        }

        public static Team Instance(string name, Color color)
        {
            return _teams.TryGetValue(name, out var team) ? team : new Team(name, color);
        }

        public static Team Join(ITeamMember member, Team newTeam)
        {
            var oldTeam = GetMembership(member);
            oldTeam._members.Remove(member);
            newTeam._members.Add(member);
            return oldTeam;
        }
        
        public static Team GetMembership(ITeamMember member)
        {
            return _teams.Values.First(t => t._members.Contains(member));
        }
    }
}