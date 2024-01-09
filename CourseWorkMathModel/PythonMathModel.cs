using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Python.Runtime;
using System.IO;
using System.Net;

namespace CourseWorkMathModel {
    class PythonMathModel {
        private List<double> _startConcentration = new();
        private List<double> _reactionSpeed = new();
        private double _contactTime;
        private const int COUNT_OF_ELEMENTS = 23;
        private const int COUNT_OF_SPEED = 21;

        public PythonMathModel(List<double> startConcentration, List<double> reactionSpeed, double contactTime) {
            _startConcentration = startConcentration;
            _reactionSpeed = reactionSpeed;
            _contactTime = contactTime;
        }



        public List<List<double>> RunScript() {
            string scriptName = "math_model.py";
            Runtime.PythonDLL = @"python-3.9.13-embed-amd64\python39.dll";
            PythonEngine.Initialize();
            List<List<double>> concentation = new();
            try {
                using (Py.GIL()) {

                    using (var scope = Py.CreateScope()) {
                        var scriptFileName = scriptName;
                        var compiledFile = PythonEngine.Compile(File.ReadAllText(scriptFileName), scriptFileName);

                        scope.Execute(compiledFile); 

                        var concentrationListPy = new PyList();
                        for (int i = 0; i < COUNT_OF_ELEMENTS; i++) {
                            concentrationListPy.Append(new PyFloat(_startConcentration[i]));
                        }

                        var speedListPy = new PyList();
                        for (int i = 0; i < COUNT_OF_SPEED; i++) {
                            speedListPy.Append(new PyFloat(_reactionSpeed[i]));
                        }

                        var contactTimePy = new PyFloat(_contactTime);


                        var result = scope.InvokeMethod("calculate_math_model", new PyObject[] { concentrationListPy, speedListPy, contactTimePy }).ToString();
                        dynamic concentrationsJson = JsonConvert.DeserializeObject(result);


                        foreach (var conc in concentrationsJson) {
                            string currentDataString = conc.Value.ToString();
    

                            currentDataString = currentDataString.Substring(5, currentDataString.Length - 8);
                            string[] currentDataArray = currentDataString.Split(",\r\n");
                            List<double> concList = new();
                            foreach (string s in currentDataArray) {
                                double parsedS = double.Parse(s);
                                if (parsedS < 0) {
                                    parsedS = 0;
                                }
                                concList.Add(parsedS);
                            }

                            concentation.Add(concList);
                        }
                        

                    }
                }

            } finally {
                PythonEngine.Shutdown();
            }

            return concentation;


        }
    }
}
