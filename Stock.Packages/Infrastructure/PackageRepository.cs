using System;
using System.Collections.Generic;
using Stock.Messages.Dto;
using Stock.Packages.Domain;

namespace Stock.Packages.Infrastructure
{
    public class PackageRepository
    {
        private readonly List<Package> _packages = new List<Package>(); 
        private int _id = 0;
        public Package PreparePackage(int productId, Guid orderId)
        {
            _id++;
            var packageSize = CalculatePackageSizeFor(productId);
            var newPackage = new Package(_id, orderId);

            newPackage.SetPackageSize(packageSize.Height, packageSize.Width, packageSize.Depth);
            newPackage.SetPackageWeigth(packageSize.Weigth);

            _packages.Add(newPackage);
            return newPackage;
        }

        public PackageSize CalculatePackageSizeFor(int productId)
        {
            //Hardcoded size for every package
            return new PackageSize
            {
                Height = 100,
                Width = 250,
                Depth = 120,
                
                Weigth = 15
            };
        }
    }
}
