# ConfigUtil
Small utility for loading/saving properties of .Net classes as string values into app.config file or other sources(Json/XML,...) 

If you want to load some properties values from app.config or any other sources like a Json string stored in DB or file ConfigUtil will do it for you in best way.

All you should do is to decorate you properties with ConfigProp attribute, you can specify default value and custom title for properties too, then you should call this.LoadConfiguration() method in class constructor any method.


    public class ImageSlider
    {
        [ConfigProp]
        public string OverlayText { get; set; }
    
        [ConfigProp(true)]//default value is true (in case of absence of this property value in app config true will be assigned)
        public bool ShowOverlayText { get; set; }
    
        [ConfigProp(5)]
        public int ImageCount { get; set; }
    
        [ConfigProp("Red")]
        public Color BgColor { get; set; }
    
        public ImageSlider(){
           this.LoadConfiguration();//loads the configuration from app.config file (appSettings section)
        }
    }
 
 * You can use other sources instead of app.config too, see Config Sources on wiki .
 * ConfigUtil supports most of common data types, to see list of supported data types see Supported Datatypes on wiki .
 * See wiki pages for more information.
