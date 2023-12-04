window.JsFunctions = {
    RegisterDeleteCustomerFunc: function (objRef) {
        var customerGuid = '888777';
        var btn = document.getElementById('btnCallCSharp');

        btn.onclick = (e) => {
            e.preventDefault();
            objRef.invokeMethodAsync('DeleteCustmer', customerGuid);
        }
    }
}
