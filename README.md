# DotMattWebUtils - Bootstrap 3 Helpers for Razor views.
HTML Helpers for Razor view input fields styled in Bootstrap 3 markup.

[Install from Nuget package](https://www.nuget.org/packages/DotMattWebUtils/)

**Text input field example:**

```
  @Html.BootstrapTextBoxFor(m => m.Firm.AccountNo, "Account No.").DivWrapper("col-md-3")
```  
> The first parameter is the model property, second parameter is the lable caption. If you omit the label caption, the field name will be used as the default label caption.

Resulting markup:
```html
<div class="col-md-3">
  <div class="form-group">
    <label for="Firm_FirmNumber">Firm No.</label>
    <input class="form-control" data-val="true" data-val-maxlength="The field FirmNumber must be a string or array type with a maximum length of &#39;5&#39;." data-val-maxlength-max="5" data-val-required="The FirmNumber field is required." id="Firm_FirmNumber" name="Firm.FirmNumber" type="text" value="001" />
  </div>
</div>
```

**Select list (Dropdown list) example:**

```
@Html.BootstrapDropDownListFor(m => m.Firm.YearEnd, Model.MonthList, "Year End").DivWrapper("col-md-3")
```
> The first parameter is the model property, second parameter is the SelectList options, and the third parameter is the lable caption. If you omit the label caption, the field name will be used as the default label caption.

Resulting markup:
```html
<div class="col-md-3">
    <div class="form-group">
        <label for="Firm_YearEnd">Year End</label>
        <select class="form-control" data-val="true" data-val-maxlength="The field YearEnd must be a string or array type with a maximum length of &#39;2&#39;." data-val-maxlength-max="2" id="Firm_YearEnd" name="Firm.YearEnd">
            <option>01</option>
            <option>02</option>
            <option>03</option>
            <option>04</option>
            <option>05</option>
            <option>06</option>
            <option>07</option>
            <option>08</option>
            <option>09</option>
            <option>10</option>
            <option>11</option>
            <option selected="selected">12</option>
        </select>
    </div>
</div>
```
