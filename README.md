# DotMattWebUtils
HTML Helpers for Razor view input fields styled in Bootstrap 3 markup.

**Text input field example:**

```
  @Html.BootstrapTextBoxFor(m => m.Firm.AccountNo, "Account No.").DivWrapper("col-md-3")
```  
> The first parameter is the model property, second parameter is the lable caption. If you omit the label caption, the field name will be used as the default label caption.

**Select list (Dropdown list) example:**

```
@Html.BootstrapDropDownListFor(m => m.Firm.BaseCurrency, Model.DefaultCurrencySelectList, "Base Currency").DivWrapper("col-md-3")
```
> The first parameter is the model property, second parameter is the SelectList options, and the third parameter is the lable caption. If you omit the label caption, the field name will be used as the default label caption.
