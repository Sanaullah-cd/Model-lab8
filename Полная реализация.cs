using System;


// 🔹 Интерфейс напитка
public interface IBeverage
{
    double GetCost();          // Получить стоимость напитка
    string GetDescription();   // Получить описание напитка
}

// 🔹 Базовый напиток — Кофе
public class Coffee : IBeverage
{
    public double GetCost() => 50.0;
    public string GetDescription() => "Coffee";
}

// 🔹 Абстрактный декоратор
public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage _beverage;
    public BeverageDecorator(IBeverage beverage) { _beverage = beverage; }

    public virtual double GetCost() => _beverage.GetCost();
    public virtual string GetDescription() => _beverage.GetDescription();
}

// 🔹 Конкретные декораторы (добавки)
public class MilkDecorator : BeverageDecorator
{
    public MilkDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost() => base.GetCost() + 10.0;
    public override string GetDescription() => base.GetDescription() + ", Milk";
}

public class SugarDecorator : BeverageDecorator
{
    public SugarDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost() => base.GetCost() + 5.0;
    public override string GetDescription() => base.GetDescription() + ", Sugar";
}

public class ChocolateDecorator : BeverageDecorator
{
    public ChocolateDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost() => base.GetCost() + 15.0;
    public override string GetDescription() => base.GetDescription() + ", Chocolate";
}

public class VanillaDecorator : BeverageDecorator
{
    public VanillaDecorator(IBeverage beverage) : base(beverage) { }
    public override double GetCost() => base.GetCost() + 8.0;
    public override string GetDescription() => base.GetDescription() + ", Vanilla";
}



// 🔹 Единый интерфейс для всех платежных систем
public interface IPaymentProcessor
{
    void ProcessPayment(double amount);   // Оплата
    void RefundPayment(double amount);    // Возврат
}

// 🔹 Внутренняя платежная система
public class InternalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing payment of {amount} via internal system.");
    }
    public void RefundPayment(double amount)
    {
        Console.WriteLine($"Refunding payment of {amount} via internal system.");
    }
}

// 🔹 Внешняя платежная система A
public class ExternalPaymentSystemA
{
    public void MakePayment(double amount)
    {
        Console.WriteLine($"Making payment of {amount} via External Payment System A.");
    }
    public void MakeRefund(double amount)
    {
        Console.WriteLine($"Making refund of {amount} via External Payment System A.");
    }
}

// 🔹 Внешняя платежная система B
public class ExternalPaymentSystemB
{
    public void SendPayment(double amount)
    {
        Console.WriteLine($"Sending payment of {amount} via External Payment System B.");
    }
    public void ProcessRefund(double amount)
    {
        Console.WriteLine($"Processing refund of {amount} via External Payment System B.");
    }
}

// 🔹 Адаптер для ExternalPaymentSystemA
public class PaymentAdapterA : IPaymentProcessor
{
    private ExternalPaymentSystemA _externalSystemA;
    public PaymentAdapterA(ExternalPaymentSystemA system) { _externalSystemA = system; }

    public void ProcessPayment(double amount) => _externalSystemA.MakePayment(amount);
    public void RefundPayment(double amount) => _externalSystemA.MakeRefund(amount);
}

// 🔹 Адаптер для ExternalPaymentSystemB
public class PaymentAdapterB : IPaymentProcessor
{
    private ExternalPaymentSystemB _externalSystemB;
    public PaymentAdapterB(ExternalPaymentSystemB system) { _externalSystemB = system; }

    public void ProcessPayment(double amount) => _externalSystemB.SendPayment(amount);
    public void RefundPayment(double amount) => _externalSystemB.ProcessRefund(amount);
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========== ☕ ДЕКОРАТОР ==========\n");

        // Создаем кофе и добавляем добавки
        IBeverage beverage = new Coffee();
        beverage = new MilkDecorator(beverage);
        beverage = new SugarDecorator(beverage);
        beverage = new ChocolateDecorator(beverage);
        beverage = new VanillaDecorator(beverage);

        Console.WriteLine($"Ваш напиток: {beverage.GetDescription()}");
        Console.WriteLine($"Итоговая стоимость: {beverage.GetCost()} руб.\n");

        Console.WriteLine("========== АДАПТЕР ==========\n");

        // Используем внутреннюю систему
        IPaymentProcessor internalProcessor = new InternalPaymentProcessor();
        internalProcessor.ProcessPayment(100);
        internalProcessor.RefundPayment(50);

        // Используем внешнюю систему A
        ExternalPaymentSystemA systemA = new ExternalPaymentSystemA();
        IPaymentProcessor adapterA = new PaymentAdapterA(systemA);
        adapterA.ProcessPayment(200);
        adapterA.RefundPayment(100);

        // Используем внешнюю систему B
        ExternalPaymentSystemB systemB = new ExternalPaymentSystemB();
        IPaymentProcessor adapterB = new PaymentAdapterB(systemB);
        adapterB.ProcessPayment(300);
        adapterB.RefundPayment(150);
    }
}
