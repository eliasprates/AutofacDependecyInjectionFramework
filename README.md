
# Autofac Dependency Injection Framework

---

## O que é o Autofac?

O Autofac é um contêiner de inversão de controle (IoC) desenvolvido para .NET, que permite a injeção de dependência em aplicações, facilitando o gerenciamento de objetos e suas dependências de forma eficiente e controlada. Ele suporta uma grande variedade de cenários, como injeção de dependências, escopos de vida (Lifetime), e integração com outras bibliotecas e frameworks, como ASP.NET Core, Web API e WCF.

### Injeção de Dependência

A injeção de dependência é um padrão de design no qual objetos recebem suas dependências de fontes externas (geralmente via um contêiner IoC) ao invés de instanciá-las diretamente. Isso facilita a testabilidade, manutenção e configuração das aplicações.

---

## O que é Lifetime no Autofac?

**Lifetime**, ou ciclo de vida de uma dependência, define o período de tempo durante o qual um objeto criado pelo contêiner IoC deve existir. Dependendo do cenário, é possível controlar como e quando essas instâncias são criadas e destruídas.

No Autofac, existem diferentes tipos de gerenciamento de ciclo de vida, chamados **Lifetime Scopes**. Esses escopos definem quando os objetos são criados e quando eles serão descartados. O conceito de "escopo" permite que um desenvolvedor controle o ciclo de vida das dependências em contextos específicos, como para uma requisição HTTP ou uma transação de banco de dados.

### Por que os escopos são importantes?

Os escopos no Autofac são fundamentais para gerenciar o ciclo de vida de objetos injetados. Eles ajudam a evitar o uso excessivo de recursos ao garantir que os objetos sejam criados e destruídos de acordo com o ciclo de vida necessário para o contexto. Isso reduz o risco de problemas de performance ou bugs relacionados ao uso incorreto de dependências (como o uso de instâncias compartilhadas inadvertidamente).

---

## Escopos Utilizados nesse Projeto

Neste projeto, foram utilizados cinco escopos importantes do Autofac, que são:

1. **Instance Per Dependency**  
   - **Descrição:** A cada vez que uma dependência é solicitada, uma nova instância será criada. Esse é o comportamento padrão no Autofac.  
   - **Uso:**  
     ```csharp
     builder.RegisterType<MyClass>().As<IMyClass>().InstancePerDependency();
     ```
   - **Exemplo no projeto:** Esse escopo é usado quando a instância de uma classe não precisa ser compartilhada e pode ser descartada logo após o uso.

2. **Single Instance**  
   - **Descrição:** Uma única instância do serviço será criada e compartilhada por todas as dependências ao longo do ciclo de vida da aplicação. Isso é semelhante ao padrão Singleton.  
   - **Uso:**  
     ```csharp
     builder.RegisterType<MyClass>().As<IMyClass>().SingleInstance();
     ```
   - **Exemplo no projeto:** É ideal para serviços que devem ter uma única instância durante toda a execução da aplicação, como o cache ou a conexão a um banco de dados de configuração global.

3. **Instance Per Lifetime Scope**  
   - **Descrição:** Uma única instância da dependência será criada por escopo. Isso significa que, se o mesmo escopo solicitar a dependência várias vezes, ela receberá a mesma instância. No entanto, diferentes escopos terão instâncias diferentes.  
   - **Uso:**  
     ```csharp
     builder.RegisterType<MyClass>().As<IMyClass>().InstancePerLifetimeScope();
     ```
   - **Exemplo no projeto:** Esse escopo é usado para cenários onde diferentes contextos precisam de uma nova instância de um serviço, como em aplicações web, onde cada requisição HTTP pode ter seu próprio escopo.

4. **Instance Per Matching Lifetime Scope**  
   - **Descrição:** Cria ou compartilha uma instância de um tipo dentro de um escopo específico que corresponde ao nome ou a uma tag. Isso é útil em cenários onde um escopo específico deve ser gerenciado separadamente de outros.  
   - **Uso:**  
     ```csharp
     builder.RegisterType<MyClass>().As<IMyClass>().InstancePerMatchingLifetimeScope("myScope");
     ```
   - **Exemplo no projeto:** Esse escopo é adequado para cenários onde é necessário gerenciar instâncias de objetos em um escopo nomeado ou para processos específicos dentro de um sistema maior.

5. **Externally Owned**  
   - **Descrição:** O Autofac cria a instância, mas não gerencia seu ciclo de vida. Isso significa que o contêiner IoC não será responsável por descartar essa instância quando o escopo de vida terminar; a responsabilidade é do código que solicitou a dependência.  
   - **Uso:**  
     ```csharp
     builder.RegisterType<MyClass>().As<IMyClass>().ExternallyOwned();
     ```
   - **Exemplo no projeto:** Ideal para cenários onde o ciclo de vida de uma instância é gerenciado fora do contêiner, como quando uma instância precisa ser descartada manualmente.

---

## Conclusão

O uso de diferentes **Lifetime Scopes** no Autofac oferece um controle refinado sobre o ciclo de vida dos objetos injetados. Neste projeto, as cinco principais opções de ciclo de vida foram utilizadas para diferentes cenários, garantindo que cada dependência seja gerenciada adequadamente e evitando desperdício de recursos.

Essa flexibilidade permite construir sistemas robustos, escaláveis e de fácil manutenção, com instâncias de objetos sendo criadas e descartadas conforme a necessidade do contexto de execução.
