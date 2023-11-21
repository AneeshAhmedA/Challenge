using Challenge.Entity;

namespace Challenge.Service
{
    public interface ISupplierService
    {
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        Supplier GetSupplier(int supplierId);
        List<Supplier> GetAllSuppliers();
        void DeleteSupplier(int supplierId);
    }
}
