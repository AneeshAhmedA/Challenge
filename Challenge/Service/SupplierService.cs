using Challenge.Database;
using Challenge.Entity;

namespace Challenge.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly MyDbContext _context;

        public SupplierService(MyDbContext context)
        {
            _context = context;
        }

        public void AddSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
        }

        public Supplier GetSupplier(int supplierId)
        {
            return _context.Suppliers.Find(supplierId);
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public void DeleteSupplier(int supplierId)
        {
            var supplier = _context.Suppliers.Find(supplierId);

            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}
