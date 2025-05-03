import { Form, Button } from 'react-bootstrap';
import { FilterState, FilterControls } from '../../hooks/useFilter';

interface FilterPanelProps<T> {
  filters: FilterState<T>;
  filterControls: FilterControls<T>;
  filterConfig: {
    [K in keyof T]: {
      label: string;
      type: 'select' | 'text' | 'number' | 'date';
      options?: { value: any; label: string }[];
    };
  };
}

export const FilterPanel = <T extends Record<string, any>>({
  filters,
  filterControls,
  filterConfig,
}: FilterPanelProps<T>) => {
  const { setFilters, setSearchQuery, resetFilters } = filterControls;

  const handleFilterChange = (key: keyof T, value: any) => {
    setFilters({ [key]: value } as Partial<T>);
  };

  return (
    <div className="card mb-4">
      <div className="card-body">
        <Form>
          <div className="row">
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Label>Пошук</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Введіть пошуковий запит..."
                  value={filters.searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                />
              </Form.Group>
            </div>

            {Object.entries(filterConfig).map(([key, config]) => (
              <div key={String(key)} className="col-md-6 mb-3">
                <Form.Group>
                  <Form.Label>{config.label}</Form.Label>
                  {config.type === 'select' && config.options ? (
                    <Form.Select
                      value={filters.filters[key]}
                      onChange={(e) => handleFilterChange(key, e.target.value)}
                    >
                      <option value="">Всі</option>
                      {config.options.map((option) => (
                        <option key={option.value} value={option.value}>
                          {option.label}
                        </option>
                      ))}
                    </Form.Select>
                  ) : (
                    <Form.Control
                      type={config.type}
                      value={filters.filters[key]}
                      onChange={(e) => handleFilterChange(key, e.target.value)}
                    />
                  )}
                </Form.Group>
              </div>
            ))}
          </div>

          <div className="d-flex justify-content-end">
            <Button
              variant="outline-secondary"
              onClick={resetFilters}
              className="me-2"
            >
              Скинути фільтри
            </Button>
          </div>
        </Form>
      </div>
    </div>
  );
}; 