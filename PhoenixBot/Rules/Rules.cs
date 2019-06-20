using System;
using System.Collections.Generic;
using Discord.WebSocket;
using System.Linq;
namespace PhoenixBot.Rules
{
    public static class Rules
    {
        internal static List<Rule> rules;
        private static string filePath = "Resources/Rules.json";

        static Rules()
        {
            if (RuleStorage.SaveFileExists(filePath))
            {
                rules = RuleStorage.LoadRuleList(filePath).ToList();
            }
            else
            {
                rules = new List<Rule>();
                SaveRules();
            }
        }
        public static void SaveRules()
        {
            RuleStorage.SaveRuleList(rules, filePath);
        }
        public static Rule GetRule(byte number)
        {
            var result = from a in rules
                         where a.RuleNumber == number
                         select a;
            var rule = result.FirstOrDefault();
            if (rule == null)
            {
                return null;
            }
            return rule;
        }
        public static Rule CreateRule(byte number, string ruleText)
        {
            var newRule = new Rule()
            {
                RuleNumber = number,
                RuleString = ruleText
            };
            rules.Add(newRule);
            SaveRules();
            return newRule;
        }
    }
}
