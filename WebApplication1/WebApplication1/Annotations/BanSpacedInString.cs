using System.ComponentModel.DataAnnotations;

namespace BSTeamSearch.Annotations
{
    public class BanSpacedInString : ValidationAttribute
    {
        public override bool IsValid(object obj)
        {
            if (obj != null)
            {
                string str = obj.ToString();

                foreach(char elem in str)
                {
                    if(elem.ToString() == " ")
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;
        }
    }
}
