using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CSVComparer.CSVComparison {

    public class CSVMappingManager {
        //================================================================================
        private List<CSVMapping>                mCSVMappingsList = new List<CSVMapping>();
        private BindingList<CSVMapping>         mCSVMappings;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVMappingManager() {
            mCSVMappings = new BindingList<CSVMapping>(mCSVMappingsList);
        }


        // MAPPINGS ================================================================================
        //--------------------------------------------------------------------------------
        public BindingList<CSVMapping> CSVMappings { get { return mCSVMappings; } }

        //--------------------------------------------------------------------------------
        public List<CSVMapping> CSVMappingsCopy {
            get {
                List<CSVMapping> csvMappingsCopy = new List<CSVMapping>();
                foreach (CSVMapping m in mCSVMappings) {
                    csvMappingsCopy.Add(new CSVMapping(m));
                }
                return csvMappingsCopy;
            }
        }
        
        //--------------------------------------------------------------------------------
        public void ClearMatchLookups() {
            foreach (CSVMapping m in mCSVMappings) {
                m.ClearMatchLookups();
            }
        }


        // SAVING / LOADING ================================================================================
        //--------------------------------------------------------------------------------
        public void SaveJSON(string path) {
            // Open
            StreamWriter streamWriter = new StreamWriter(path);
            JsonTextWriter writer = new JsonTextWriter(streamWriter);

            // Formatting
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            // Start
            writer.WriteStartObject();

            // Mappings
            writer.WritePropertyName("Mappings");
            writer.WriteStartArray();
            foreach (CSVMapping m in mCSVMappings) {
                writer.WriteStartObject();
                m.SaveJSON(writer);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            // End
            writer.WriteEndObject();

            // Close
            streamWriter.Close();
        }
        
        //--------------------------------------------------------------------------------
        public void LoadJSON(string path) {
            // Reset
            mCSVMappings.Clear();

            // Open
            try {
                StreamReader streamReader = new StreamReader(path);
                string json = streamReader.ReadToEnd();
                streamReader.Close();

                // Parse
                JObject jsonObject = JObject.Parse(json);

                // Mappings
                JArray mappings = (JArray)jsonObject.SelectToken("Mappings");
                if (mappings != null) {
                    foreach (JToken m in mappings) {
                        mCSVMappings.Add(new CSVMapping(m));
                    }
                }
            }
            catch (FileNotFoundException) { }
            catch (DirectoryNotFoundException) { }
        }

    }

}
