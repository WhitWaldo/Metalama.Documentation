﻿namespace Doc.ValidateAfterAllAspects
{

    [AddLogger]
    internal class OkClass
    {
        [Log]
        private void Bar() { }
    }

    internal class ErrorClass
    {
        [Log]
        private void Bar() { }
    }
}
