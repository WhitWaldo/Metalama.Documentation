﻿using Metalama.Documentation.QuickStart;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDemo
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender project)
        {
            //Locating all types 
            var allTypes = project.Outbound.SelectMany
                            (p => p.Types);

            //Finding all public methods from all types
            var allPublicMethods = allTypes
                                    .SelectMany(t => t.Methods)
                                    .Where(z => z.Accessibility == Accessibility.Public);

            //Adding Log aspect 
            allPublicMethods.AddAspectIfEligible<LogAttribute>();
        }
    }

}
