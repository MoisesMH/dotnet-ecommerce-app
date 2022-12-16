using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using AutoMapper;
using Core.Entities;

namespace api.Helpers;

// Source: where do we map from? Product
// Destination: where we want to map to? ProductToResolveDto
// We want some destination prosperty to be a string
public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            // If PictureUrl field is not null or empty
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config.GetValue<string>("ApiUrl") + source.PictureUrl;
            }
            // Else
            return null;
        }
    }