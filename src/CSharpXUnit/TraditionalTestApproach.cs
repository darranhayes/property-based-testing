namespace CSharpXUnit;

public class TraditionalTestApproach
{
    [Fact]
    public void ReducesNormalItemQualityBy1()
    {
        var normalItem = new Item { Name = "Normal Item", Quality = 10, SellIn = 10 };

        var service = new GildedRose(new List<Item> { normalItem });
        service.UpdateQuality();
        Assert.Equal(9, normalItem.Quality);
    }

    [Fact]
    public void Bug_ReducesNormalItemSellInBy1()
    {
        var normalItem = new Item { Name = "Normal Item", Quality = 10, SellIn = 10 };

        var service = new GildedRose(new List<Item> { normalItem });
        service.UpdateQuality();

        // this test is defective because we're asserting on the wrong property (copy-paste behaviour)
        // selecting the same values for Quality and SellIn has hidden the issue
        Assert.Equal(9, normalItem.Quality);
    }

    [Fact(Skip = "Fails fast, as test is asserting on the wrong property")]
    public void Fails_ReducesNormalItemSellInBy1()
    {
        // by varying the values of Quality and SellIn, the test fails - unexpectedly
        // highlighting that there must be a problem
        var normalItem = new Item { Name = "Normal Item", Quality = 10, SellIn = 4 };

        var service = new GildedRose(new List<Item> { normalItem });
        service.UpdateQuality();

        Assert.Equal(3, normalItem.Quality);
    }

    [Fact]
    public void Fix_ReducesNormalItemSellInBy1()
    {
        // by varying the values of Quality and SellIn, we're forced to fix the defective test
        var normalItem = new Item { Name = "Normal Item", Quality = 10, SellIn = 4 };

        var service = new GildedRose(new List<Item> { normalItem });
        service.UpdateQuality();

        Assert.Equal(3, normalItem.SellIn);
    }

}
