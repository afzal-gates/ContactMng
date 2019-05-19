﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ContactManagement.DAL
{
    public class CustomParameterList : System.Collections.ArrayList
    {
        private ArrayList parameterNames = new ArrayList();
        private ArrayList dbTypes = new ArrayList();
        private ArrayList paramValues = new ArrayList();
        private int index = 0;

        public ArrayList ParameterNames
        {
            get { return this.parameterNames; }
        }

        public ArrayList ParameterTypes
        {
            get { return this.dbTypes; }
        }

        public ArrayList ParameterValues
        {
            get { return this.paramValues; }
        }

        public void Add(CustomParameter param)
        {
            this.parameterNames.Add(param.ParamaterName);
            this.dbTypes.Add(param.ParamType);
            this.paramValues.Add(param.ParamValue);

        }
        public void Remove(CustomParameter param)
        {
            this.parameterNames.Remove(param.ParamaterName);
            this.dbTypes.Remove(param.ParamType);
            this.paramValues.Remove(param.ParamValue);
        }

    }
}
