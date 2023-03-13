using System;
using System.Collections.Generic;
using System.Text;

namespace Pet_As_Service.APIService
{
    public class CatModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string temperament { get; set; }
        public string origin { get; set; }
        public string description { get; set; }

    }

    public class FavoriteCat
    {
        public string id { get; set; }
        public string image_id { get; set; }
    }

    public class CatStored
    {
        public string id { get; set; }
        public string name { get; set; }
    }

} //Fim

