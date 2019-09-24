using System.ComponentModel.DataAnnotations;

namespace Vue.Core.Data
{
    public class Enums
    {
        public enum ApiPolicy
        {
           Read,
           Write,
           Delete,
           Print
        }

        public enum Gender
        {
            Male,Female,other
        }
    }
}