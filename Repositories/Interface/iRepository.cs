namespace MVC75NET.Repositories.Interface
{
interface iRepository <Key, Entity> where Entity : class
    {
        //Get All
        List<Entity> GetAll();

        //Get By Id 
        Entity GetById(Key key);

        //Insert 
        int Insert (Entity entity);

        //Update 
        int Update (Entity entity);

        //Delete 
        int Delete (Key key);
    }
}
