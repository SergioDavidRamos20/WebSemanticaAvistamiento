﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace DAL
{
   public class TodoDAL
    {
        private static string baseUrl = @"http://localhost:3030/primerDB/sparql";
        public List<TodoEntidad> Listar(string buscar)
        {
            var TodoEntidadLista = new List<TodoEntidad>();
        
            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/prueba/sparql"));

            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
                "SELECT   ?subject ?predicate ?object " +
                "WHERE" +
                "{" +
                "?subject ?predicate ?object." +
                " FILTER(REGEX(STR(?subject),'"+buscar+"','i')) }"
                );
            SparqlResultSet results2 = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
                "SELECT   ?subject ?predicate ?object " +
                "WHERE" +
                "{" +
                "?subject ?predicate ?object." +
                " FILTER(REGEX(STR(?predicate),'" + buscar + "','i')) }"
                );
            SparqlResultSet results3 = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
                "SELECT   ?subject ?predicate ?object " +
                "WHERE" +
                "{" +
                "?subject ?predicate ?object." +
                " FILTER(REGEX(STR(?object),'" + buscar + "','i')) }"
                );
            //foreach (SparqlResult result in results)
            //{
            //    Console.WriteLine(result.ToString());
            //}
            //tomar columnas
            TodoEntidad o = new TodoEntidad();
            //var li1 = results.Variables;
            //foreach (string s in li1)
            //{

            //}
            //tomar registros
            var li2 = results.Results;
            var li3 = results.Results;
            var li4 = results.Results;
            foreach (var s in li2)
            {
                TodoEntidad on = new TodoEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());
                     
                }

                on.subject = lista[0].ToString();
                on.predicate = lista[1].ToString();
                 on.Object = lista[2].ToString();
                TodoEntidadLista.Add(on);
            }
            foreach (var s in li3)
            {
                TodoEntidad on = new TodoEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.subject = lista[0].ToString();
                on.predicate = lista[1].ToString();
                on.Object = lista[2].ToString();
                TodoEntidadLista.Add(on);
            }
            foreach (var s in li4)
            {
                TodoEntidad on = new TodoEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.subject = lista[0].ToString();
                on.predicate = lista[1].ToString();
                on.Object = lista[2].ToString();
                TodoEntidadLista.Add(on);
            }
            //


            return TodoEntidadLista;
        }
    }
}
