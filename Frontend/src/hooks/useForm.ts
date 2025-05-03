import { useState, useCallback } from 'react';

type FormState<T> = {
  values: T;
  errors: Partial<Record<keyof T, string>>;
  touched: Partial<Record<keyof T, boolean>>;
};

type ValidationRules<T> = {
  [K in keyof T]?: (value: T[K]) => string | null;
};

export const useForm = <T extends Record<string, any>>(
  initialValues: T,
  validationRules?: ValidationRules<T>
) => {
  const [formState, setFormState] = useState<FormState<T>>({
    values: initialValues,
    errors: {},
    touched: {},
  });

  const handleChange = useCallback((name: keyof T, value: T[keyof T]) => {
    setFormState(prev => ({
      ...prev,
      values: {
        ...prev.values,
        [name]: value,
      },
      touched: {
        ...prev.touched,
        [name]: true,
      },
    }));
  }, []);

  const validateField = useCallback((name: keyof T, value: T[keyof T]) => {
    if (!validationRules?.[name]) return null;
    return validationRules[name]!(value);
  }, [validationRules]);

  const validateForm = useCallback(() => {
    const errors: Partial<Record<keyof T, string>> = {};
    let isValid = true;

    Object.keys(formState.values).forEach(key => {
      const fieldName = key as keyof T;
      const error = validateField(fieldName, formState.values[fieldName]);
      if (error) {
        errors[fieldName] = error;
        isValid = false;
      }
    });

    setFormState(prev => ({
      ...prev,
      errors,
    }));

    return isValid;
  }, [formState.values, validateField]);

  const handleBlur = useCallback((name: keyof T) => {
    const error = validateField(name, formState.values[name]);
    setFormState(prev => ({
      ...prev,
      errors: {
        ...prev.errors,
        [name]: error,
      },
      touched: {
        ...prev.touched,
        [name]: true,
      },
    }));
  }, [formState.values, validateField]);

  const resetForm = useCallback(() => {
    setFormState({
      values: initialValues,
      errors: {},
      touched: {},
    });
  }, [initialValues]);

  return {
    values: formState.values,
    errors: formState.errors,
    touched: formState.touched,
    handleChange,
    handleBlur,
    validateForm,
    resetForm,
  };
}; 