using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Source.Gadgeteers.Game.Entities;
using Source.Gadgeteers.UI;
using UnityEngine;

namespace Source.Gadgeteers.Game.Items
{
    [RequireComponent(typeof(StatController))]
    public abstract class Item : MonoBehaviour, ITooltipContext
    {
        public const string Path = "Items/";
        
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _abilityName;
        [SerializeField]
        [TextArea]
        private string _description;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private List<Modifier> _passiveModifiers;

        public string Name => _name;
        
        public string AbilityName => _abilityName;

        public string Description => _description;

        public Sprite Sprite => _sprite;
        
        public StatController StatCtrl { get; private set; }
        
        public Player Owner => GetComponentInParent<Player>();

        protected void Awake()
        {
            StatCtrl = GetComponent<StatController>();
            if(_sprite == null) _sprite = Resources.Load<Sprite>("Textures/MissingTexture");
        }

        public Modifier[] GetModifiers()
        {
            _passiveModifiers.Sort();
            return _passiveModifiers.ToArray();
        }

        public string GetContent()
        {
            var text = $"<style=Name><style=Header>{Name}</style></style>";
            
            if (this is Weapon)
            {
                text +=
                    "\n\n{@attack_speed}\n" +
                    "{@attack_range}\n" +
                    "{@power}";
            }
            
            if (_passiveModifiers.Count > 0)
            {
                text += "\n";
                foreach (var mod in GetModifiers())
                {
                    var prefix = mod.Arg >= 0 ? "+" : "";
                    switch (mod.Operation)
                    {
                        case Operation.Add:
                            prefix += $"{mod.Arg}";
                            break;
                        case Operation.AddMultiplied:
                            prefix += $"{mod.Arg * 100}%";
                            break;
                    }
                    text += $"\n{{{prefix}}} {{#{mod.Target}}}";
                }
            }

            text += $"\n\n<style=Name>{AbilityName}</style>" +
                    $"\n<style=Normal>{Description}</style>";

            
            text = Regex.Replace(text, "{@" + KeyString.Format + "}", Evaluator1);
            text = Regex.Replace(text, "{#" + KeyString.Format + "}", Evaluator2);
            text = Regex.Replace(text, "{[^{}]*}", Evaluator3);
            
            string Evaluator1(Match match)
            {
                KeyString key = match.Value[2..^1];
                if (!StatCtrl.TryGet(key, out var stat)) return match.Value;
                
                if(stat.Dependencies.Length == 0) return $"{{{stat.BaseValue.Round(2)}}} {{#{key}}}";
                
                List<string> ingredients = new();
                if(stat.BaseValue != 0) ingredients.Add($"{{{stat.BaseValue}}}");
                foreach (var dep in stat.Dependencies)
                {
                    ingredients.Add($"{{{dep.Factor.Round(2) * 100}%}}{{#{dep.Target.Root}}}");
                }

                return $"({string.Join(" + ", ingredients)} = {{{StatCtrl.Compute(stat, true).Round(2)}}}) {{#{key}}}";
            }
            string Evaluator2(Match match)
            {
                KeyString key = match.Value[2..^1];
                if (!StatCtrl.TryGet(key, out var stat)) return match.Value;
                
                string root = key.Root;
                string flags = string.Join("", ((DamageType)stat.Flags).GetActiveFlags().Select(s => $"<sprite name=\"{s.ToString()}\" tint=1>"));
                return $"<style={root}>{flags}</style>";
            }
            string Evaluator3(Match match)
            {
                return $"<style=hl>{match.Value[1..^1]}</style>";
            }

            return text;
        }
    }
}