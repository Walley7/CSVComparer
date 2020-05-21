using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CSVComparer.CSVComparison {

    public class CSVColumn {
        //================================================================================
        private string                          mName;

        private bool                            mVisible = true;


        //================================================================================
        //--------------------------------------------------------------------------------
        public CSVColumn(string name) {
            mName = name;
        }


        // COLUMN ================================================================================
        //--------------------------------------------------------------------------------
        public string Name { get { return mName; } }
        public bool Visible { set { mVisible = value; } get { return mVisible; } }


        // STRING ================================================================================
        //--------------------------------------------------------------------------------
        public override string ToString() { return mName; }
    }

}
