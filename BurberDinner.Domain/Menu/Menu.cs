
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Menu.Entities;
using BurberDinner.Domain.Menu.ValueObjects;

namespace BurberDinner.Domain.Menu
{
  public sealed class Menu : AggregateRoot<MenuId>
  {
    private readonly List<MenuSection> _section = new ();
    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; } 
    public IReadOnlyList<MenuSection> Sections => _section;
  }
}