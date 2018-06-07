using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using DataAccess;

namespace DataAccessTest
{
    [TestFixture]
    public class TesteCrudSimples
    {
        private Animal animalTestes = new Animal();

        public TesteCrudSimples()
        {
            
            animalTestes.Nome = "cavalo";
            animalTestes.Descricao = "quadrupedes";

        }
        
        [Test]
        public void A_Create()
        {
            AnimalDao dao = new AnimalDao();

            try
            {
                dao.Insert(animalTestes);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
                Console.WriteLine(e);
                throw;
            }

        }

        [Test]
        public void B_Read()
        {
            AnimalDao animalDao = new AnimalDao();
            
            Animal teste = animalDao.Select(animalTestes).First();

            Console.WriteLine($"{teste.Nome}, {teste.Descricao}");
            Console.WriteLine($"{animalTestes.Nome}, {animalTestes.Descricao}");

            bool verdadeiro = (
                (teste.Nome == animalTestes.Nome) &&
                (teste.Descricao == animalTestes.Descricao)
            );
            
            Assert.IsTrue(verdadeiro);

        }

        [Test]
        public void C_Update()
        {
            AnimalDao dao = new AnimalDao();
            animalTestes = dao.Select(animalTestes).First();
            animalTestes.Descricao = "não tem";
            
            dao.Update(animalTestes);

            Animal animal = dao.Select(animalTestes).First();

            bool verdadeiro = (
                (animal.Nome == animalTestes.Nome) &&
                (animal.Descricao == animalTestes.Descricao)
            );
            
            Assert.IsTrue(verdadeiro);
            
            

        }

        [Test]
        public void D_Delete()
        {
            AnimalDao dao = new AnimalDao();

            animalTestes = dao.Select(animalTestes).First();
            
            dao.Delete(animalTestes.Id);

            Assert.IsTrue(dao.Select(animalTestes).First().Nome == "NONE");
        }
    }
}