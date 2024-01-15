# Projeto Dominando Testes de Software em .NET Core 8

Bem-vindo ao projeto desenvolvido no curso "Dominando Testes de Software" na plataforma desenvolvedor.io. Este projeto é uma adaptação do curso original ministrado em C# .NET Core 2.2, sendo atualizado para .NET Core 8. Aqui você encontrará um conjunto abrangente de testes divididos em quatro partes principais: Testes de Unidade, TDD (Desenvolvimento Orientado a Testes), Testes de Integração e BDD (Desenvolvimento Orientado a Comportamento).

## Estrutura do Projeto

O projeto está organizado em quatro partes principais, cada uma focada em uma área específica de testes:

1. **Testes de Unidade:**
   - Utilização do xUnit para criar testes de unidade.
   - Uso de Fixtures para gerenciar o estado compartilhado entre testes.
   - Organização dos testes com a utilização de Order.
   - Criação de dados realistas com Bogus.
   - Mocking eficiente com Moq e Moq.AutoMock.
   - Utilização do FluentAssertion para tornar os testes mais expressivos.
   - Mensagens de saída detalhadas com FluentAssertion.
   - Estratégias de escape com Skip para gerenciar testes específicos.

2. **TDD (Desenvolvimento Orientado a Testes):**
   - Implementação de TDD para desenvolver software com base em testes.
   
3. **Testes de Integração:**
   - Utilização de xUnit para criar testes de integração.
   - Integração com Selenium para testes de interface.
   - Playlist personalizada de testes.
   - Relatório de cobertura de testes com Fine Code Coverage.

4. **BDD (Desenvolvimento Orientado a Comportamento):**
   - Testes automatizados usando SpecFlow.xUnit.
   - Integração com Selenium e ChromeDriver.

## Recursos Explorados

O projeto explora uma variedade de recursos e ferramentas, incluindo:

- **xUnit:** Estrutura de teste para .NET.
- **Fixtures:** Gerenciamento de estados compartilhados.
- **Order:** Organização de testes.
- **Bogus:** Geração de dados realistas.
- **Moq e Moq.AutoMock:** Ferramentas de mocking.
- **FluentAssertion:** Melhora a legibilidade dos testes.
- **Skip:** Estratégia de escape para testes específicos.
- **TDD com Selenium:** Testes de integração com Selenium.
- **BDD com SpecFlow.xUnit:** Desenvolvimento orientado a comportamento.
- **AngleSharp:** Testes de interface com AngleSharp.
- **Selenium e ChromeDriver:** Ferramentas para automação de testes de interface.

## Importante: Versão do ChromeDriver

Certifique-se de que a versão do ChromeDriver seja compatível com a versão do pacote de testes da solução. Incompatibilidades podem causar erros durante a execução dos testes. Mantenha a versão do ChromeDriver alinhada com a versão da solução de teste.

## Como Iniciar

1. **Pré-requisitos:**
   - Certifique-se de ter o .NET Core 8 instalado em sua máquina.
   - Instale as dependências do projeto.

2. **Executando os Testes:**
   - Execute os testes de unidade, TDD, testes de integração e BDD para garantir a integridade do sistema.

3. **Relatório de Cobertura:**
   - Utilize o Fine Code Coverage para gerar relatórios detalhados sobre a cobertura de testes.

## Considerações finais


Obrigado por explorar este projeto! Esperamos que seja uma experiência educativa e prática no mundo dos testes de software.
