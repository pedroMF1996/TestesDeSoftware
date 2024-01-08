Funcionalidade: Usuario - Login
	Como um usuario 
	Eu desejo realizar o login 
	Para que eu possa acessar as demais funcionalidades

Cenário: Realizar login com sucesso
Dado Que um usuario esta acessando o site da loja
Quando Ele clicar em login
E Preencher os dados do formulario com uma senha sem caracteres especiais
	| Dados					|
	| Email					|
	| Senha					|
E Clicar no botao Login
Então Ele sera redirecionado para a vitrine
E Uma saudacao com seu e-mail sera exibida no menu superior