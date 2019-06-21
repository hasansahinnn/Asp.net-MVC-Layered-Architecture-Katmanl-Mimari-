# Asp.net-MVC-Layered-Architecture-Katmanl-Mimari

Download Files and Add DAL and REPO (Solution --> Add --> Existing Project  Choose Both)

Edit DAL file  for your data models.

Add this part UnitOfWork.cs For Every Data Models on DAL;

private Repository<Model1> _Model1;

        public Repository<Model1> Model1   /* Model1 your model class name */
        {
            get
            {
                if (_Model1 == null)
                {
                    _Model1 = new Repository<Model1>(context);
                }
                return _Model1;
            }
        }
        

For use;

using (UnitOfWork work = new UnitOfWork())
{
      work.Model1.find(5);
}

Don't Forget! Web.Config Connection string name should be "DBContextUsage"
