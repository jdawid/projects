using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskBusinessUserObjects
{
    public delegate void RuleBrokenEventHandler(bool blnIsBroken); // template for event  

    /// <summary>
    /// Broken Rules Class - maintain rules for helpdesk
    /// </summary>
    public class BrokenRules
    {
        /// <summary>
        /// event based on delegate to indicate whether or not rule is broken
        /// true = rule still broken
        /// false = rule requirements are satisfied
        /// </summary>
        public event RuleBrokenEventHandler evntStillBroken; // event
        private Dictionary<string,bool> rules;

        /// <summary>
        /// Constructor
        /// </summary>
        public BrokenRules() // set up hashtable container for rules
        {
            rules = new Dictionary<string,bool>();
        }

        /// <summary>
        /// main method adds or removes rules from hashtable
        /// </summary>
        /// <param name="strProperty">name of the rule</param>
        /// <param name="blnBroken">still broken or not</param>
        public void MaintainRule(string strProperty, bool blnBroken)
        {
            try
            {
                if (blnBroken)
                { // add or update container
                    if (rules.ContainsKey(strProperty))
                        rules[strProperty] = blnBroken;
                    else
                        rules.Add(strProperty, blnBroken);
                }
                else // remove from container
                    rules.Remove(strProperty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            } // do nothing
            finally
            {
                if (rules.Count > 0)          // zero means Object has valid state
                    evntStillBroken(true);    // something is still broken
                else
                    evntStillBroken(false);   // everything is good
            }
        }
    }
}
