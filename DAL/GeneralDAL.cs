using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace DAL
{
   public class GeneralDal
    {
        private static string baseUrl = @"http://localhost:3030/primerDB/sparql";
        public List<GeneralEntidad> Listar(string buscar)
        {
            var GeneralEntidadLista = new List<GeneralEntidad>();

            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/AvesOntology/sparql"));

            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/sergioramos/ontologies/2022/6/untitled-ontology-10#>" +
                "SELECT   ?Paciente ?AtendidoPor ?FechaIngreso ?Edad ?EstaUbicadoEn " +
                "WHERE { " +
                "?x data:NOMBRE_AVE ?Paciente. " +
                "?x data:PERTENECE_A_UNA ?AtendidoPor. " +
                "?x data:ALIMENTACION ?Edad. " +
                "?x data:ESTA_UBICADO_EN ?EstaUbicadoEn. " +
                "FILTER(REGEX(str(?Paciente),'" + buscar + "','i')) }"
               );

            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            GeneralEntidad o = new GeneralEntidad();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                GeneralEntidad on = new GeneralEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.Paciente = lista[0].Substring(lista[0].IndexOf('#')+1);
                on.AtendidoPor = lista[1].Substring(lista[1].IndexOf('#') + 1);
                //on.FechaIngreso = lista[2].Substring(lista[2].IndexOf('#') + 1);
                //on.Edad = lista[2].Substring(0,lista[3].IndexOf('^'));
                on.Edad = lista[2];
                on.EstaUbicadoEn = lista[3].Substring(lista[3].IndexOf('#') + 1);
                GeneralEntidadLista.Add(on);
            }
            //


            return GeneralEntidadLista;
        }
    }
}
