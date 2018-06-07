using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class AnimalDao
    {
        private DataBase _database;
        
        public AnimalDao()
        {
           _database = new DataBase("127.0.0.1", "root","","animais"); 
            
        }

        public void Insert(Animal animal)
        {
            string query = $"insert into animal (nome, descricao) values ('{animal.Nome}','{animal.Descricao}')";
            
            _database.Command(query);
        }

        public List<Animal> Select(Animal animal)
        {
            string query =
                $"select * from animal where nome like '%{animal.Nome}%' and descricao like '%{animal.Descricao}%'";
            MySqlDataReader reader = _database.CommandQuery(query);
            
            List<Animal> animais = new List<Animal>();
            
            if (reader.HasRows) {
                
                while (reader.Read()){
                    
                    animais.Add(new Animal()
                    {
                        Id = (int) reader["id"],
                        Nome = reader["nome"].ToString(),
                        Descricao = reader["descricao"].ToString()
                    });
                }
            }
            else
            {
                animais.Add(new Animal{Nome = "NONE",Descricao = "NONE"});
            }

            _database.CloseConnection();
            
            return animais;
        }

        public void Update(Animal animal)
        {
            var query = $"update animal set nome='{animal.Nome}', descricao='{animal.Descricao}' where id={animal.Id}";
            
            _database.Command(query);
        }

        public void Delete(int id)
        {
            var query = $"delete from animal where id='{id}'";
            
            
            _database.Command(query);
        }
    }
}