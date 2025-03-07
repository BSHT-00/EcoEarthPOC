using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Pages.Scanner
{
    public class ScannerMoreInfoUtils
    {

        // Checks whether materials in a product can be recycled 
        private static bool DoItRecycle(string Material)
        {
            string test = File.ReadAllText("EcoEarthPOC/Components/Pages/Scanner/Data/Lists/RecyclableMaterials.txt");

            if (File.ReadAllText("EcoEarthPOC/Components/Pages/Scanner/Data/Lists/RecyclableMaterials.txt").Contains(Material))
            {
                return true;
            }
            else
                return false;
        }

        // Initialises a list with the recyclable materials inside a product and includes a bool detailing whether it can be recycled
        public static List<EcoEarthPOC.Components.Pages.Scanner.Data.ViewModels.RecylableMaterialVM> InitialiseRecycleList(List<EcoEarthPOC.Components.Pages.Scanner.Data.DTOs.GetMorePackagingInfoDTO.Packagings> packagings)
        {
            List<EcoEarthPOC.Components.Pages.Scanner.Data.ViewModels.RecylableMaterialVM> RecyclableMaterials = new List<EcoEarthPOC.Components.Pages.Scanner.Data.ViewModels.RecylableMaterialVM>();

            if (packagings == null)
            {
                return RecyclableMaterials;
            }

            try
            {
                foreach (var packaging in packagings)
                {
                    RecyclableMaterials.Add(new EcoEarthPOC.Components.Pages.Scanner.Data.ViewModels.RecylableMaterialVM
                    {
                        Material = packaging.Material,
                        Shape = packaging.Shape,
                        Recyclable = DoItRecycle(packaging.Material)
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return RecyclableMaterials;
        }
    }
}
