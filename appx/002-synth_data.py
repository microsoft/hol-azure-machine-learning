import numpy as np
import matplotlib.pyplot as plt
import csv
from itertools import izip

x = range(1, 31)
y = x

noise = np.random.uniform(-1, 1, 30)

ywnoise = y + noise * 2

plt.plot(x, ywnoise)
plt.show()

with open('linoise.csv', 'wb') as f:
    writer = csv.writer(f)
    writer.writerow(['x','','ywnoise'])
    writer.writerows(izip(x, ywnoise))
    