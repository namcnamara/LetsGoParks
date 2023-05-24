using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGoPark.WebSite.Models
{
    /// <summary>
    /// This enum holds ParkSystems
    /// </summary>
    //Enum list holds the possiblilites for city, state, and national systems
    public enum ParkSystemEnum
    {
        City = 0,
        State = 1,
        National = 2,
    }
    
    public static class ParkSystemEnumExtensions
    {
        //Displays the data of the Enum
        public static string DisplayName(this ParkSystemEnum data)
        {
            //Return switch for string representation
            return data switch
            {
                ParkSystemEnum.City => "City Parks",
                ParkSystemEnum.State => "WA State Parks",
                ParkSystemEnum.National => "National Parks",

                //Default value if nothing is specified
                _ => "WA State Parks",
            };
        }
    }
}