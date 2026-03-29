// Date: 2025-07-17



using System;



namespace Rune
{
    public class NullableFunc<TReturn>
    {
        public NullableFunc(Func<TReturn> func = null, TReturn defaultValue = default)
        {
            Func = func;

            DefaultValue = defaultValue;
        }

        public NullableFunc(TReturn defaultValue = default) : this(null, defaultValue)
        {

        }



        public TReturn Invoke()
        {
            return Func != null ? Func.Invoke() : DefaultValue;
        }



        public Func<TReturn> Func { get; set; } = null;

        public TReturn DefaultValue { get; set; } = default;
    }



    public class NullableFunc<TArg, TReturn>
    {
        public NullableFunc(Func<TArg, TReturn> func = null, TReturn defaultValue = default)
        {
            Func = func;

            DefaultValue = defaultValue;
        }

        public NullableFunc(TReturn defaultValue = default) : this(null, defaultValue)
        {

        }



        public TReturn Invoke(TArg arg)
        {
            return Func != null ? Func.Invoke(arg) : DefaultValue;
        }



        public Func<TArg, TReturn> Func { get; set; } = null;

        public TReturn DefaultValue { get; set; } = default;
    }



    public class NullableFunc<TArg0, TArg1, TReturn>
    {
        public NullableFunc(Func<TArg0, TArg1, TReturn> func = null, TReturn defaultValue = default)
        {
            Func = func;

            DefaultValue = defaultValue;
        }

        public NullableFunc(TReturn defaultValue = default) : this(null, defaultValue)
        {

        }



        public TReturn Invoke(TArg0 arg0, TArg1 arg1)
        {
            return Func != null ? Func.Invoke(arg0, arg1) : DefaultValue;
        }



        public Func<TArg0, TArg1, TReturn> Func { get; set; } = null;

        public TReturn DefaultValue { get; set; } = default;
    }



    public class NullableFunc<TArg0, TArg1, TArg2, TReturn>
    {
        public NullableFunc(Func<TArg0, TArg1, TArg2, TReturn> func = null, TReturn defaultValue = default)
        {
            Func = func;

            DefaultValue = defaultValue;
        }

        public NullableFunc(TReturn defaultValue = default) : this(null, defaultValue)
        {

        }



        public TReturn Invoke(TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            return Func != null ? Func.Invoke(arg0, arg1, arg2) : DefaultValue;
        }



        public Func<TArg0, TArg1, TArg2, TReturn> Func { get; set; } = null;

        public TReturn DefaultValue { get; set; } = default;
    }



    /* Example of NullableFunc user */

    public class NullableFuncUserExample
    {
        public Func<object> FuncGetObject
        {
            get => NullableFuncGetObject.Func;
            set => NullableFuncGetObject.Func = value;
        }



        public NullableFunc<object> NullableFuncGetObject { get; } = new(null);
    }
}