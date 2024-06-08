

using BurberDinner.Domain.HostAggregate.ValueObjects;
using BurberDinner.Domain.MenuAggregate.Entities;
using BurberDinner.Domain.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurberDinner.Infrastructure.Persistence.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            ConfigureMenuTable(builder);
            ConfigureMenuSectionsTable(builder);
            ConfigureMenuDinnerIdsTable(builder);
            ConfigureMenuReviewIdsTable(builder);
        }

        private void ConfigureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.MenuReviewIds, dib =>
            {
                dib.ToTable("MenuReviewIds");

                dib.WithOwner().HasForeignKey("MenuId");

                dib.HasKey("Id");

                dib.Property(d => d.Value)
                   .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureMenuDinnerIdsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.DinnerIds, dib =>
            {
                dib.ToTable("MenuDinnerIds");

                dib.WithOwner().HasForeignKey("MenuId");

                dib.HasKey("Id");

                dib.Property(d => d.Value)
                   .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.Sections, sb =>
            {
                sb.ToTable("MenuSections");

                sb.WithOwner().HasForeignKey("MenuId");

                sb.HasKey("Id", "MenuId");

                sb.Property(s => s.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value, value => MenuSectionId.CreateUnique());

                sb.Property(s => s.Name)
                    .HasMaxLength(100);

                sb.Property(s => s.Description)
                    .HasMaxLength(1000);

                sb.OwnsMany(s => s.Items, ib =>
                {
                    ib.ToTable("MenuItems");

                    ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                    ib.HasKey("MenuId", "MenuSectionId");

                    ib.Property(i => i.Id)
                       .ValueGeneratedNever()
                       .HasConversion(
                            id => id.Value,
                            value => MenuItemId.CreateUnique()
                       );

                    ib.Property(i => i.Name)
                       .HasMaxLength(100);

                    ib.Property(i => i.Description)
                       .HasMaxLength(1000);
                });

                sb.Navigation(s => s.Items).Metadata.SetField("_items");
                sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            builder.Metadata.FindNavigation(nameof(Menu.Sections))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureMenuTable(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => MenuId.CreateUnique());

            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(m => m.HostId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => HostId.CreateUnique());
        }
    }
}

