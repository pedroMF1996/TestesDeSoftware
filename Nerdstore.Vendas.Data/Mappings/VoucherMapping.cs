﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerdstore.Vendas.Domain.Entidades;

namespace Nerdstore.Vendas.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Voucher : Pedidos
            builder.HasMany(c => c.Pedidos)
                .WithOne(c => c.Voucher)
                .HasForeignKey(c => c.VoucherId);

            builder.ToTable("Vouchers");
        }
    }
}