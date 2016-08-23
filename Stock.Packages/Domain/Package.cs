using System;
using Stock.Messages.Dto;

namespace Stock.Packages.Domain
{
    public class Package
    {
        private Guid _orderId;
        public Package(int packageId, Guid orderId)
        {
            PackageId = packageId;
            _orderId = orderId;
        }

        private double _height;
        private double _width;
        private double _depth;
        private double _weigth;

        public int PackageId { get; }

        public void SetPackageSize(double height, double width, double depth)
        {
            _height = height;
            _width = width;
            _depth = depth;
        }

        public void SetPackageWeigth(double weigth)
        {
            _weigth = weigth;
        }

        public string SizeAndWeigth => 
            $"heigth: {_height}, width: {_width}, depth: {_depth}, weigth: {_weigth}";

        public PackageSize Size => new PackageSize {Height = _height, Width = _width, Depth = _depth, Weigth = _weigth};
    }
}
