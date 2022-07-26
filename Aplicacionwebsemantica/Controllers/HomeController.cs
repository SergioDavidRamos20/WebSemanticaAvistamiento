
using Aplicacionwebsemantica.Models;
using Entidades;
using LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VDS.RDF.Query;

namespace Aplicacionwebsemantica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/AvesOntology/sparql"));

            List<Ave> listaAves = new List<Ave>();
            SparqlResultSet resultados = endpoint.QueryWithResultSet(
                "PREFIX data: <http://www.semanticweb.org/sergioramos/ontologies/2022/6/untitled-ontology-10#> " +
                "PREFIX owl: <http://www.w3.org/2002/07/owl#> " +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> " +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "PREFIX xml: <http://www.w3.org/XML/1998/namespace/> " +
                "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> " +
                "SELECT ?ave " +
                "WHERE { " +
                  "?x data:NOMBRE_AVE ?ave. " +
                "}"
                );

            foreach (var result in resultados.Results)
            {
                Ave ave = new Ave();
                var datos = result.ToList();
                ave.Nombre = datos[0].Value.ToString();
                listaAves.Add(ave);
            }

            return View(listaAves);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult ListarTodo()
        {
           TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.Listar(""));
        }
        [HttpPost]
        public ActionResult ListarTodo(string buscar)
        {

            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.Listar(buscar));

        }
        public ActionResult ListarClase()
        {
            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarClase(""));
        }
        [HttpPost]
        public ActionResult ListarClase(string buscar)
        {

            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarClase(buscar));

        }
        public ActionResult ListarSubClase()
        {
            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarSubClase(""));
        }
        [HttpPost]
        public ActionResult ListarSubClase(string buscar)
        {

            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarSubClase(buscar));

        }
        public ActionResult ListarGeneral()
        {
            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarGeneral(""));
        }
        [HttpPost]
        public ActionResult ListarGeneral(string buscar)
        {

            TodoLogicaNegocio ot = new TodoLogicaNegocio();
            return View(ot.ListarGeneral(buscar));

        }
     
        public ActionResult ListaParques()
        {
            var HospitalEntidadLista = new List<HospitalEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/AvesOntology/sparql"));
            string buscar = "";
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/sergioramos/ontologies/2022/6/untitled-ontology-10#>" +
                "SELECT       ?NombreHospital ?SituadoEn " +
                "WHERE { " +
                "?x data:NOMBRE_PARQUE ?NombreHospital. " +
                "?x data:SITUADO_EN ?SituadoEn. " +

                "FILTER(REGEX(str(?NombreHospital),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            GeneralEntidad2 o = new GeneralEntidad2();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                HospitalEntidad on = new HospitalEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreHospital = lista[0];
                on.SituadoEn = lista[1].Substring(lista[1].IndexOf('#')+1);
                //on.CodigoHospital = lista[2].Substring(0, lista[2].IndexOf('^')); 

                HospitalEntidadLista.Add(on);
            }

            return View(HospitalEntidadLista);

        }



        /*
        [HttpPost]
        public ActionResult ListarHospital(string buscar)
        {
            var HospitalEntidadLista = new List<HospitalEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/prueba/sparql"));
             
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/alberto/ontologies/2022/2/untitled-ontology-33#>" +
                "SELECT       ?NombreHospital ?SituadoEn ?CodigoHospital " +
                "WHERE { " +
                "?x data:NombreHospital ?NombreHospital. " +
                "?x data:SituadoEn ?SituadoEn. " +
                "?x data:CodigoHospital ?CodigoHospital. " +

                "FILTER(REGEX(str(?NombreHospital),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            GeneralEntidad2 o = new GeneralEntidad2();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                HospitalEntidad on = new HospitalEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreHospital = lista[0];
                on.SituadoEn = lista[1].Substring(lista[1].IndexOf('#')+1);
                on.CodigoHospital = lista[2].Substring(0, lista[2].IndexOf('^'));

                HospitalEntidadLista.Add(on);
            }

            return View(HospitalEntidadLista);

        }
        */

        public ActionResult ListarAvistador()
        {
            var MedicoEntidadLista = new List<MedicoEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/AvesOntology/sparql"));
            string buscar = "";
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/sergioramos/ontologies/2022/6/untitled-ontology-10#>" +
                "SELECT       ?NombreMedico ?TrabajaEn ?CodigoMedico " +
                "WHERE { " +
                "?x data:NOMBRE_VISTADOR ?NombreMedico. " +
                "?x data:LE_INTERESA ?TrabajaEn. " +
                "?x data:EDAD_VISTADOR ?CodigoMedico. " +

                "FILTER(REGEX(str(?NombreMedico),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            MedicoEntidad o = new MedicoEntidad();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                MedicoEntidad on = new MedicoEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreMedico = lista[0];
                on.TrabajaEn = lista[1].Substring(lista[1].IndexOf('#')+1);
                on.CodigoMedico = lista[2].Substring(0, lista[2].IndexOf('^'));

                MedicoEntidadLista.Add(on);
            }

            return View(MedicoEntidadLista);


        }
        [HttpPost]
        public ActionResult ListarMedico(string buscar)
        {
            var MedicoEntidadLista = new List<MedicoEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/prueba/sparql"));
            
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/alberto/ontologies/2022/2/untitled-ontology-33#>" +
                "SELECT       ?NombreMedico ?TrabajaEn ?CodigoMedico " +
                "WHERE { " +
                "?x data:NombreMedico ?NombreMedico. " +
                "?x data:TrabajaEn ?TrabajaEn. " +
                "?x data:CodigoMedico ?CodigoMedico. " +

                "FILTER(REGEX(str(?NombreMedico),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            MedicoEntidad o = new MedicoEntidad();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                MedicoEntidad on = new MedicoEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreMedico = lista[0];
                on.TrabajaEn = lista[1].Substring(lista[1].IndexOf('#')+1);
                on.CodigoMedico = lista[2].Substring(0, lista[2].IndexOf('^'));

                MedicoEntidadLista.Add(on);
            }

            return View(MedicoEntidadLista);


        }

        public ActionResult ListarFamiliaUbicacion()
        {
            var EnfermedadEntidadLista = new List<EnfermedadEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/AvesOntology/sparql"));
            string buscar = "";
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/sergioramos/ontologies/2022/6/untitled-ontology-10#>" +
                "SELECT    ?CodigoEnfermedad ?NombreEnfermedad ?TieneSintoma " +
                "WHERE { " +
                "?x data:NOMBRE_PARQUE ?NombreEnfermedad. " +
                "?x data:SITUADO_EN ?TieneSintoma. " +
                "?x data:CONTIENE ?CodigoEnfermedad. " +

                "FILTER(REGEX(str(?NombreEnfermedad),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            EnfermedadEntidad o = new EnfermedadEntidad();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                EnfermedadEntidad on = new EnfermedadEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreEnfermedad = lista[0].Substring(lista[0].IndexOf('#') + 1);
                on.TieneSintoma = lista[1];
                on.CodigoEnfermedad = lista[2].Substring(lista[2].IndexOf('#')+1);

                EnfermedadEntidadLista.Add(on);
            }

            return View(EnfermedadEntidadLista);


        }

        [HttpPost]
        public ActionResult ListarEnfermedad(string buscar)
        {
            var EnfermedadEntidadLista = new List<EnfermedadEntidad>();


            SparqlRemoteEndpoint endpoint2 = new SparqlRemoteEndpoint(new Uri("http://localhost:3030/prueba/sparql"));
             
            SparqlResultSet results = endpoint2.QueryWithResultSet("PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>" +
               "PREFIX owl: <http://www.w3.org/2002/07/owl#>" +
               "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>" +
               "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> " +
                "prefix data:<http://www.semanticweb.org/alberto/ontologies/2022/2/untitled-ontology-33#>" +
                "SELECT     ?NombreEnfermedad ?TieneSintoma ?CodigoEnfermedad " +
                "WHERE { " +
                "?x data:NombreEnfermedad ?NombreEnfermedad. " +
                "?x data:TieneSintoma ?TieneSintoma. " +
                "?x data:CodigoEnfermedad ?CodigoEnfermedad. " +

                "FILTER(REGEX(str(?NombreEnfermedad),'" + buscar + "','i')) }"
               );


            foreach (SparqlResult result in results)
            {
                Console.WriteLine(result.ToString());
            }
            //tomar columnas
            EnfermedadEntidad o = new EnfermedadEntidad();
            var li1 = results.Variables;
            foreach (string s in li1)
            {

            }
            //tomar registros
            var li2 = results.Results;
            foreach (var s in li2)
            {
                EnfermedadEntidad on = new EnfermedadEntidad();
                var lista = new List<string>();
                foreach (var resul in s)
                {
                    if (resul.Value != null)
                        lista.Add(resul.Value.ToString());

                }

                on.NombreEnfermedad = lista[0];
                on.TieneSintoma = lista[1].Substring(lista[1].IndexOf('#') + 1);
                on.CodigoEnfermedad = lista[2].Substring(0, lista[2].IndexOf('^'));

                EnfermedadEntidadLista.Add(on);
            }

            return View(EnfermedadEntidadLista);


        }
    }
}