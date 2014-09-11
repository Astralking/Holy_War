using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Holy_War.Enumerations
{
    public enum Team
    {
		[Description("Neutral")]
		Neutral = 0,
        [Description("Blue")]
        Blue = 1,
        [Description("Red")]
        Red = 2
    }
}
