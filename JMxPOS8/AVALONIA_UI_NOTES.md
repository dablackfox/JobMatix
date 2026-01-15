# Avalonia UI Implementation Notes

## Critical: DataGrid vs ListBox

### ⚠️ DO NOT USE DataGrid - Use ListBox Instead

**Issue:** Avalonia's DataGrid has binding issues with ObservableCollections in our MVVM setup. Items do not display properly even with correct bindings.

**Solution:** Use ListBox with ItemTemplate instead. This provides:
- ✅ Reliable MVVM binding with ObservableCollections
- ✅ Full control over item display layout
- ✅ Better performance
- ✅ Easier styling

### Pattern to Follow

**❌ BAD - DataGrid (doesn't work):**
```xml
<DataGrid ItemsSource="{Binding Items}" 
          AutoGenerateColumns="False">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}"/>
        <DataGridTextColumn Header="Value" Binding="{Binding Value}"/>
    </DataGrid.Columns>
</DataGrid>
```

**✅ GOOD - ListBox with ItemTemplate:**
```xml
<ListBox ItemsSource="{Binding Items}"
         SelectedItem="{Binding SelectedItem}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Border BorderBrush="LightGray" BorderThickness="0,0,0,1" Padding="5">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="200"/>
                        <ColumnDefinition Width="*"/>
                    </Grid.ColumnDefinitions>
                    <TextBlock Grid.Column="0" Text="{Binding Name}" Margin="5,0"/>
                    <TextBlock Grid.Column="1" Text="{Binding Value}" Margin="5,0"/>
                </Grid>
            </Border>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### Known DataGrid Issues in Current Code

**⚠️ Need to Fix:**
1. **Reports Tab** - DataGrid used for report data display (NOT WORKING)
2. **Transactions Tab** - DataGrid may be used (CHECK)
3. **Any future grids** - Must use ListBox pattern

### Successfully Converted Examples

✅ **Stock Tab** - Converted from DataGrid to ListBox (Lines ~262-440 in MainWindow.axaml)
✅ **Customers Tab** - Converted from DataGrid to ListBox (Lines ~502-700)
✅ **Sale Items** - Converted from DataGrid to ListBox (Lines ~158-195)

### Additional ListBox Tips

1. **Headers:** Use a separate Grid above the ListBox for column headers
```xml
<!-- Header Row -->
<Grid Background="LightGray" Margin="0,0,0,2">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    <TextBlock Grid.Column="0" Text="Name" Margin="5" FontWeight="Bold"/>
    <TextBlock Grid.Column="1" Text="Value" Margin="5" FontWeight="Bold"/>
</Grid>

<!-- Data ListBox -->
<ListBox ItemsSource="{Binding Items}">
    ...
</ListBox>
```

2. **Selection:** ListBox handles selection better than DataGrid
3. **Performance:** ListBox virtualizes better with large datasets
4. **Styling:** Easier to customize appearance per item
5. **Events:** Click events work reliably on ListBox items

### Why This Happened

DataGrid in Avalonia 11.x has known issues with:
- Complex binding scenarios
- ObservableCollection change notifications
- Column auto-generation with custom types
- Selection binding in MVVM patterns

The Avalonia team recommends using ListBox for most list scenarios.

## Other UI Notes

### Auto-Loading Read-Only Detail Views

Pattern used in Stock and Customers tabs:
- Detail panel always visible
- Fields use `IsReadOnly="{Binding !IsEditing}"` 
- Fields use `IsHitTestVisible="{Binding IsEditing}"` to prevent clicking when read-only
- Auto-loads when item selected via `OnSelectedItemChanged` partial method

### TextBox Watermarks

Use `Watermark` property instead of `PlaceholderText`:
```xml
<TextBox Text="{Binding Value}" Watermark="Enter value..."/>
```

### Command Bindings from DataTemplates

To access parent ViewModel from inside a DataTemplate:
```xml
<Button Command="{Binding $parent[Window].DataContext.SomeCommand}" 
        CommandParameter="{Binding}"/>
```

### Date/Time Controls

Use `DatePicker` and `TimePicker` separately:
```xml
<DatePicker SelectedDate="{Binding Date}"/>
<TimePicker SelectedTime="{Binding Time}"/>
```

Don't use `DateTimePicker` (not available in Avalonia).
