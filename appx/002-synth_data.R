# Generate synthetic data
x <- seq(1, 30)
y <- x
noise <- runif(30, -1, 1)
ywnoise <- y + noise * 2

# plot point cloud on a chart
plot(x, ywnoise, xlab = NA, ylab = NA)

# combine two columns to create data grid
linoise <- cbind(x, ywnoise)

# write out to a CSV file
write.csv(linoise, file = "linoise.csv", row.names = FALSE)