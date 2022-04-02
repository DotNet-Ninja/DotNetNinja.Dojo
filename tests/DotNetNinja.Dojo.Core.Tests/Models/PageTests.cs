using System.Collections.Generic;
using System.Linq;

using DotNetNinja.Dojo.Constants;
using DotNetNinja.Dojo.Models;

using FluentAssertions;

using Xunit;

namespace DotNetNinja.Dojo.Core.Tests.Models;

public class PageTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetInitializeItemsAsEmptyList()
    {
        var page = new Page<Entity>();

        page.Items.Should().NotBeNull();
        page.Items.Should().BeEmpty();
    }

    [Fact]
    public void ParameterizedConstructor_ShouldInitializeNumberToExpectedValue()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;

        var page = new Page<Entity>(pageNumber, pageSize, total, GenerateEntities(pageSize, 51));

        page.Number.Should().Be(pageNumber);
    }

    [Fact]
    public void ParameterizedConstructor_ShouldInitializeSizeToExpectedValue()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;

        var page = new Page<Entity>(pageNumber, pageSize, total, GenerateEntities(pageSize, 51));

        page.Size.Should().Be(pageSize);
    }

    [Fact]
    public void ParameterizedConstructor_ShouldInitializeTotalItemCountToExpectedValue()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;

        var page = new Page<Entity>(pageNumber, pageSize, total, GenerateEntities(pageSize, 51));

        page.TotalItemCount.Should().Be(total);
    }

    [Fact]
    public void ParameterizedConstructor_ShouldInitializeItemsWithExpectedEntities()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 51);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.Items.Should().BeSameAs(entities);
    }

    [Fact]
    public void CanGoForward_WhenMorePages_ShouldBeTrue()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 51);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.CanGoForward.Should().BeTrue();
    }

    [Fact]
    public void CanGoForward_WhenLastPage_ShouldBeFalse()
    {
        const int pageNumber = 4;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 76);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.CanGoForward.Should().BeFalse();
    }

    [Fact]
    public void CanGoBack_WhenFirstPages_ShouldBeFalse()
    {
        const int pageNumber = 1;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.CanGoBack.Should().BeFalse();
    }

    [Fact]
    public void CanGoBack_WhenNotFirstPage_ShouldBeTrue()
    {
        const int pageNumber = 4;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 76);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.CanGoBack.Should().BeTrue();
    }

    [Fact]
    public void HasMultiplePage_WhenMoreThanOnePage_ShouldBeTrue()
    {
        const int pageNumber = 4;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 76);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.HasMultiplePages.Should().BeTrue();
    }

    [Fact]
    public void HasMultiplePage_WhenOnePage_ShouldBeFalse()
    {
        const int pageNumber = 1;
        const int pageSize = 25;
        const int total = 12;
        var entities = GenerateEntities(pageSize);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.HasMultiplePages.Should().BeFalse();
    }

    [Fact]
    public void NextPage_WhenMorePages_ShouldBeOneMoreThanCurrentPage()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 51);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.NextPage.Should().Be(pageNumber + 1);
    }
    
    [Fact]
    public void NextPage_WhenNoMorePages_ShouldBeSameAsCurrentPage()
    {
        const int pageNumber = 4;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 76);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.NextPage.Should().Be(pageNumber);
    }

    [Fact]
    public void PreviousPage_WhenNotFirstPage_ShouldBeOneLessThanCurrentPage()
    {
        const int pageNumber = 3;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize, 51);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.PreviousPage.Should().Be(pageNumber - 1);
    }

    [Fact]
    public void PreviousPage_WhenFirstPage_ShouldBeSameAsCurrentPage()
    {
        const int pageNumber = 1;
        const int pageSize = 25;
        const int total = 100;
        var entities = GenerateEntities(pageSize);

        var page = new Page<Entity>(pageNumber, pageSize, total, entities);

        page.PreviousPage.Should().Be(pageNumber);
    }

    private List<Entity> GenerateEntities(int qty, int start = 1)
    {
        var entities = 
        Enumerable.Range(start, qty).Select(id => new Entity
        {
            MetaData = new MetaData
            {
                Annotations = new Dictionary<string, string>
                {
                    {
                        "custom-annotation", $"value-{id}"
                    }
                },
                Description = $"Entity{id} Description",
                Labels = new Dictionary<string, string>
                {
                    {
                        "type", "entity"
                    }
                },
                Location = new Location
                {
                    Scheme = LocationScheme.Dojo,
                    Identifier = Location.None
                },
                Name = $"Entity{id}",
                Tags = new List<string> {"TestEntity"}
            }
        }).ToList();
        return entities;
    }
}