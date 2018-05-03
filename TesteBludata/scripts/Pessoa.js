function SalvarPessoa() {

    //Nome
    var nome = $("#nome").val();

    //CPF
    var cpf = $("#cpf").val();

    //Data do Cadastro
    var dataCad = $("#data_cad").val();

    //Data de Nascimento
    var dataNasc = $("#data_nasc").val();

    //Rg
    var rg = $("#rg").val();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/Pessoa/Create"] input[name="__RequestVerificationToken"]').val();

    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    var url = "/Pessoa/Create";

    $.ajax({
        url: url,
        type: "POST",
        datatype: "JSON",
        headers: headersadr,
        data: { id: 0, nome: nome, cpf: cpf, data_cad: dataCad, data_nasc: dataNasc, rg: rg, __RequestVerificationToken: token },
        success: function (data) {
            if (data.Resultado > 0) {
                ListarTelefones(data.Resultado);
            }
        }
    })    
}

function ListarTelefones(idPessoa) {

    var url = "/Telefone/ListarTelefones";

    $.ajax({
        url: url,
        type: "GET",
        data: { id: idPessoa },
        datatype: "JSON",
        success: function (data) {
            var divTelefones = $("#divTelefones");
            divTelefones.empty();
            divTelefones.show();
            divTelefones.html(data);
        }
    })
}

function AddTelefone() {
    debugger;
    var telefone = $("#Telefone").val();
    var idPessoa = $("#idPessoa").val();

    var url = "/Telefone/AddTelefone";

    $.ajax({
        url: url,
        data: { idPessoa: idPessoa, num_tel: telefone},
        type: "GET",
        datatype: "json",
        success: function (data) {
            if (data.Resultado > 0) {
                ListarTelefones(idPessoa);
            }
        }
    })
}
