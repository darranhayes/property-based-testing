namespace FsCheckTests;

using FsCheck;
using FsCheck.Xunit;
using GildedRose;

public class PropertyBasedTestApproach
{
    [Property]
    public void Example_ReducesNormalItemQualityBy1(StringNoNulls name, PositiveInt sellin)
    {
        var item = new Item { Name = name.Get, Quality = 10, SellIn = sellin.Get };
        var service = new GildedRose(new List<Item> { item });
        service.UpdateQuality();
        Assert.Equal(9, item.Quality);
    }

    [Property]
    public void Property_ReducesNormalItemQualityBy1(StringNoNulls name, PositiveInt quality, PositiveInt sellin)
    {
        var item = new Item { Name = name.Get, Quality = quality.Get, SellIn = sellin.Get };
        var service = new GildedRose(new List<Item> { item });
        service.UpdateQuality();
        Assert.Equal(quality.Get - 1, item.Quality);
    }

    [Property(Skip = "Demo: This copy-pasted test fails correctly as we've not updated the assertion to check the SellIn property")]
    public void Bug_Example_ReducesNormalItemSellInBy1(StringNoNulls name, PositiveInt quality)
    {
        var item = new Item { Name = name.Get, Quality = quality.Get, SellIn = 10 };
        var service = new GildedRose(new List<Item> { item });
        service.UpdateQuality();
        Assert.Equal(9, item.Quality); // Fails here
    }

    [Property]
    public void Fix_Example_ReducesNormalItemSellInBy1(StringNoNulls name, PositiveInt quality)
    {
        var item = new Item { Name = name.Get, Quality = quality.Get, SellIn = 10 };
        var service = new GildedRose(new List<Item> { item });
        service.UpdateQuality();
        Assert.Equal(9, item.SellIn);
    }

    [Property]
    public void Fix_Property_ReducesNormalItemSellInBy1(StringNoNulls name, PositiveInt quality, PositiveInt sellin)
    {
        var item = new Item { Name = name.Get, Quality = quality.Get, SellIn = sellin.Get };
        var service = new GildedRose(new List<Item> { item });
        service.UpdateQuality();
        Assert.Equal(sellin.Get - 1, item.SellIn);
    }
}